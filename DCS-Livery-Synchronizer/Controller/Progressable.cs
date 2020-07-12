using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS_Livery_Synchronizer
{
    public abstract class Progressable
    {
        public event Action<float> OnProgressUpdate;

        protected void SetProgress(float progress)
        {
            this.OnProgressUpdate?.Invoke(progress);
        }
    }

    public class Downloader : Progressable
    {
        private readonly RestClient Client;

        public async Task<byte[]> Run(Livery livery)
        {

        }
    }
    public class Unpacker : Progressable
    {
        public FileData[] Run(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
    public class Resizer : Progressable
    {
        public FileData[] Run(FileData[] files)
        {
            throw new NotImplementedException();
        }
    }

    public class FileData
    {
        public string FileName;
        public byte[] Bytes;
    }
}
