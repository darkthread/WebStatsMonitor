using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace WebStatsMonitor
{
    public class EmbResVirtualPathProvider : VirtualPathProvider
    {
        static Dictionary<string, string> virPathToEmbRes =
            typeof(EmbResVirtualPathProvider).Assembly.GetManifestResourceNames()
            .ToDictionary(
                o => o.Replace("WebStatsMonitor.Views", "~.EmbViews").ToLower(),
                o => o
                );

        public static bool IsEmbResPath(string virtualPath) =>
            VirtualPathUtility.ToAppRelative(virtualPath).StartsWith("~/EmbViews/");
        public static string FindEmbResFullName(string virtualPath)
        {
            if (!IsEmbResPath(virtualPath)) return null;
            virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
            var mappedName = virtualPath.Replace("/", ".").ToLower();
            if (virPathToEmbRes.ContainsKey(mappedName))
                return virPathToEmbRes[mappedName];
            return null;
        }

        public override bool FileExists(string virtualPath)
        {
            return 
                (IsEmbResPath(virtualPath) && FindEmbResFullName(virtualPath) != null) ||
                base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (!IsEmbResPath(virtualPath))
                return base.GetFile(virtualPath);
            var embResFullName = FindEmbResFullName(virtualPath);
            if (embResFullName == null)
                throw new HttpException((int)HttpStatusCode.NotFound,
                    "Rebedded Resource not found");
            return new EmbResVirtualFile(virtualPath, FindEmbResFullName(virtualPath));
            
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsEmbResPath(virtualPath)) return null;
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

    }
}
