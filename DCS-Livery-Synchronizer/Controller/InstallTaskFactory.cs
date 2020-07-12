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
            string installDir = $"{ this.Model.Settings.dcssavedgames.TrimEnd('/', '\\')}\\Liveries\\{livery.path}";

            var downloader = new Downloader(livery, this.Model.GetOnlineRepoBaseAddress());
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

            var downloadTask = new Task<byte[]>(() => downloader.Run());
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

            Stack<Exception> exceptions = new Stack<Exception>();
            exceptions.Push(task.Exception);

            while (exceptions.Count > 0)
            {
                var ex = exceptions.Pop();
                if (ex is TaskCanceledException)
                {
                    // TaskCanceledException is just telling us a task wasn't run... But otherwise nothing useful.
                    var tc = ex as TaskCanceledException;
                    if (tc.Task.Exception != null)
                    {
                        exceptions.Push(tc.Task.Exception);
                    }
                    continue;
                }

                errorMessage.AppendLine($"[{ex.GetType().Name}: {ex.Message}]");
                errorMessage.AppendLine(ex.StackTrace);

                if (ex is AggregateException)
                {
                    var ag = ex as AggregateException;
                    for (int i = ag.InnerExceptions.Count - 1; i >= 0; i--)
                    {
                        exceptions.Push(ag.InnerExceptions[i]);
                    }
                }
                else
                {
                    if (ex.InnerException != null)
                    {
                        exceptions.Push(ex.InnerException);
                    }
                }
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
        private readonly Livery Livery;
        private readonly string Address;

        public Downloader(Livery livery, string address)
        {
            _ = livery ?? throw new ArgumentNullException(nameof(livery));
            this.Livery = livery;
            this.Address = address;
        }

        public byte[] Run()
        {
            this.SetProgress(InstallState.Download, 0);

            string url = $"{this.Address.TrimEnd('/')}/{this.Livery.url}";

            var client = new WebClient();
            client.DownloadProgressChanged += this.OnProgressUpdated;
            var task = client.DownloadDataTaskAsync(url);
            task.Wait();
            client.DownloadProgressChanged -= this.OnProgressUpdated;

            if (task.Status != TaskStatus.RanToCompletion)
            {
                throw task.Exception;
            }

            this.SetProgress(InstallState.Download, 1);
            return task.Result;
        }

        private void OnProgressUpdated(object sender, DownloadProgressChangedEventArgs e)
        {
            this.SetProgress(InstallState.Download, e.ProgressPercentage / 100f);
        }
    }
    public class Unpacker : Progressable
    {
        public FileData[] Run(byte[] zippedArchieveBytes)
        {
            _ = zippedArchieveBytes ?? throw new ArgumentNullException(nameof(zippedArchieveBytes));

            this.SetProgress(InstallState.Unpack, 0);

            //File.WriteAllBytes($@"C:\TestOutput\{Guid.NewGuid().ToString()}", zippedArchieveBytes);

            using (var archiveStream = new MemoryStream(zippedArchieveBytes))
            using (var archive = Ionic.Zip.ZipFile.Read(archiveStream))
            {
                int count = archive.Count;
                //var tasks = new Task<FileData>[count];

                List<FileData> fileList = new List<FileData>();

                // TODO: Taskify this for SPEEEED ?
                for (int i = 0; i < count; i++)
                {
                    this.SetProgress(InstallState.Unpack, (float)i / count);

                    var entry = archive[i];
                    if (entry.IsDirectory)
                    {
                        continue;
                    }

                    using (var data = new MemoryStream())
                    {
                        entry.Extract(data);
                        fileList.Add( new FileData(entry.FileName, data.ToArray()) );
                    }
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
            object sync = new object();

            for (int i = 0; i < count; i++)
            {
                var file = files[i];
                tasks[i] = Task.Run(() =>
                {
                    string installPath = $"{this.DestinationFolder}\\{file.FileName}";
                    var dir = Path.GetDirectoryName(installPath);

                    // Don't know if this lock is necessary here, but I've added it for good measure.
                    lock (sync)
                    {
                        if (Directory.Exists(dir) == false)
                        {
                            Directory.CreateDirectory(dir); 
                        }
                    }

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
