using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCS_Livery_Synchronizer
{
    public class InstallTaskFactory
    {
        private readonly Model Model;

        public InstallTaskFactory(Model model)
        {
            this.Model = model;
            
        }

        public Task CreateDownloadAndInstallTask(Livery livery, Action<InstallState, float> onProgressUpdate)
        {
            string installDir = $"{this.Model.Settings.dcssavedgames.TrimEnd('/', '\\')}\\Liveries\\{livery.path}";

            var client = new RestClient(this.Model.GetOnlineRepoBaseAddress());
            client.AddDefaultHeader("Accept", "*/*");

            var downloader = new Downloader(client);
            var unpacker = new Unpacker();
            var resizer = new Resizer();
            var writer = new FileWriter(installDir);

            float progress = 0;
            if (onProgressUpdate != null)
            {
                downloader.OnProgressUpdate     += (s, p) => { progress = 0.00f + p * 0.75f; onProgressUpdate(s, progress); };
                unpacker.OnProgressUpdate       += (s, p) => { progress = 0.75f + p * 0.05f; onProgressUpdate(s, progress); };
                resizer.OnProgressUpdate        += (s, p) => { progress = 0.80f + p * 0.15f; onProgressUpdate(s, progress); };
                writer.OnProgressUpdate         += (s, p) => { progress = 0.95f + p * 0.05f; onProgressUpdate(s, progress); };
            }

            var downloadTask = new Task<byte[]>(() => downloader.Run(livery));
            downloadTask.ContinueWith(t => { onProgressUpdate?.Invoke(InstallState.Error, progress); this.OnFailed(t); }, TaskContinuationOptions.OnlyOnFaulted);

            var unpackTask = downloadTask.ContinueWith(f => unpacker.Run(f.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
                ;
            unpackTask.ContinueWith(t => { onProgressUpdate?.Invoke(InstallState.Error, progress); this.OnFailed(t); }, TaskContinuationOptions.OnlyOnFaulted);

            var resizeTask = unpackTask.ContinueWith(f => resizer.Run(f.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
                ;
            resizeTask.ContinueWith(t => { onProgressUpdate?.Invoke(InstallState.Error, progress); this.OnFailed(t); }, TaskContinuationOptions.OnlyOnFaulted);

            var writeTask = resizeTask.ContinueWith(f => writer.Run(f.Result), TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(w => onProgressUpdate(InstallState.Done, 1), TaskContinuationOptions.OnlyOnRanToCompletion);
            ;
            writeTask.ContinueWith(t => { onProgressUpdate?.Invoke(InstallState.Error, progress); this.OnFailed(t); }, TaskContinuationOptions.OnlyOnFaulted);

            downloadTask.Start();

            return writeTask;
        }

        private void OnFailed(Task task)
        {
            StringBuilder errorMessage = new StringBuilder();
            Exception ex = task.Exception;
            while (ex != null)
            {
                if (ex is AggregateException)
                {
                    var ag = ex as AggregateException;
                    errorMessage.AppendLine($"AGGREGATE EXCEPTION: {ex.Message}");
                    foreach (var e in ag.InnerExceptions)
                    {
                        errorMessage.AppendLine($"- [{e.GetType().Name}: {e.Message}]");
                        errorMessage.AppendLine(e.StackTrace);
                    }

                    ex = ag.InnerException; // Skip 1 exception.
                }
                else
                {
                    errorMessage.AppendLine($"[{ex.GetType().Name}: {ex.Message}]");
                    errorMessage.AppendLine(ex.StackTrace);
                }

                ex = ex.InnerException;
            }

            MessageBox.Show(errorMessage.ToString(), "UNHANDLED EXCEPTION");
        }
    }

    public enum InstallState
    {
        Error = -1,
        NotStarted = 0,
        Download,
        Unpack,
        Resize,
        Write,
        Done,
    }

    public abstract class Progressable
    {
        public event Action<InstallState, float> OnProgressUpdate;

        protected void SetProgress(InstallState state, float progress)
        {
            this.OnProgressUpdate?.Invoke(state, progress);
        }
    }

    public class Downloader : Progressable
    {
        private readonly RestClient Client;

        public Downloader(RestClient client)
        {
            this.Client = client;
        }

        public byte[] Run(Livery livery)
        {
            _ = livery ?? throw new ArgumentNullException(nameof(livery));

            this.SetProgress(InstallState.Download, 0);

            var request = new RestRequest(livery.url);
            var response = this.Client.Execute(request, Method.GET);

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception($"{(int)response.StatusCode}: {response.StatusCode.ToString()}");

            var data = response.RawBytes;

            this.SetProgress(InstallState.Download, 1);
            return data;
        }
    }
    public class Unpacker : Progressable
    {
        public FileData[] Run(byte[] zippedArchieveBytes)
        {
            _ = zippedArchieveBytes ?? throw new ArgumentNullException(nameof(zippedArchieveBytes));

            this.SetProgress(InstallState.Unpack, 0);

            object sync = new object();

            int completedFiles = 0;

            using (var archiveStream = new MemoryStream(zippedArchieveBytes))
            using (ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Read))
            {
                int count = archive.Entries.Count;
                var tasks = new Task<FileData>[count];

                for (int i = 0; i < count; i++)
                {
                    var entry = archive.Entries[i];
                    tasks[i] = Task.Run<FileData>(() =>
                    {
                        // Skip directories.
                        if (entry.FullName[entry.FullName.Length - 1] == '/' || entry.FullName[entry.FullName.Length - 1] == '\\')
                        {
                            return null;
                        }

                        using (var zippedFile = entry.Open())
                        using (var data = new MemoryStream())
                        {
                            zippedFile.CopyTo(data);
                            return new FileData(entry.FullName, data.ToArray());
                        }
                    })
                    .ContinueWith(f =>
                    {
                        lock (sync)
                        {
                            completedFiles++;
                            this.SetProgress(InstallState.Unpack, (float)completedFiles / count);
                        }
                        return f.Result;
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                    ;
                }

                Task.WaitAll(tasks);

                if (completedFiles != count)
                {
                    // TODO: Something gone did wrong!
                }

                List<FileData> fileList = new List<FileData>();
                for (int i = 0; i < count; i++)
                {
                    if (tasks[i].Result != null) fileList.Add(tasks[i].Result);
                }

                var files = new FileData[fileList.Count];
                fileList.CopyTo(files);

                this.SetProgress(InstallState.Unpack, 1);
                return files;
            }
        }
    }
    public class Resizer : Progressable
    {
        public FileData[] Run(FileData[] files)
        {
            _ = files ?? throw new ArgumentNullException(nameof(files));

            this.SetProgress(InstallState.Resize, 0);

            this.SetProgress(InstallState.Resize, 1);
            return files;
        }
    }
    public class FileWriter : Progressable
    {
        private readonly string DestinationFolder;

        public FileWriter(string destinationFolder)
        {
            this.DestinationFolder = destinationFolder.TrimEnd('/', '\\');
        }

        public FileData[] Run(FileData[] files)
        {
            _ = files ?? throw new ArgumentNullException(nameof(files));

            this.SetProgress(InstallState.Write, 0);

            // Delete any existing installation.
            if (Directory.Exists(this.DestinationFolder))
            {
                Directory.Delete(this.DestinationFolder, true);
            }
            Directory.CreateDirectory(this.DestinationFolder);

            int count = files.Length;
            var tasks = new Task[count];

            for (int i = 0; i < count; i++)
            {
                var file = files[i];
                tasks[i] = Task.Run(() =>
                {
                    string installPath = $"{this.DestinationFolder}\\{file.FileName}";
                    var dir = Path.GetDirectoryName(installPath);
                    // TODO: Could we be creating the same directory at the same time here? Could that be an issue?
                    // Might need to sync this.
                    if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir); 

                    File.WriteAllBytes(installPath, file.Bytes);
                });
            }

            Task.WhenAll(tasks);

            this.SetProgress(InstallState.Write, 1);

            return files;
        }
    }

    public class FileData
    {
        public readonly string FileName;
        public readonly byte[] Bytes;

        public FileData(string fileName, byte[] bytes)
        {
            this.FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            this.Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
        }

        public override string ToString()
        {
            return this.FileName;
        }
    }
}
