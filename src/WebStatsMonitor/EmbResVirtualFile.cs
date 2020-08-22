using System.IO;
using System.Web.Hosting;

namespace WebStatsMonitor
{
    public class EmbResVirtualFile : VirtualFile
    {
        private readonly string embResFullName;

        public EmbResVirtualFile(string virtualPath, string embResFullName) : base(virtualPath)
        {
            this.embResFullName = embResFullName;
        }

        public override Stream Open()
        {
            return this.GetType().Assembly.GetManifestResourceStream(embResFullName);
        }
    }
}
