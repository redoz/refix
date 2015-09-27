using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refix
{
    public class Workspace : IDisposable
    {
        private readonly string _path;

        public Bundle Bundle { get; }

        public static async Task<Workspace> CreateAsync(Bundle bundle)
        {
            Workspace ret = new Workspace(bundle);
            await ret.DeployAsync();
            return ret;
        }

        public Workspace(Bundle bundle)
        {
            this._path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n"));
            Directory.CreateDirectory(this._path);
            this.Bundle = bundle;
        }

        private async Task DeployAsync()
        {
            // TODO exec these in parallel?
            foreach (Resource resource in this.Bundle)
                await resource.CopyToAsync(this._path);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                try
                {
                    Directory.Delete(this._path, true);
                }
                catch (Exception ex)
                {
                    // TODO log this   
                }
            }
        }

        ~Workspace()
        {
            this.Dispose(false);
        }
    }
}