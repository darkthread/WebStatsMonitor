using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebStatsMonitor
{
    public class WebStatsMonitorController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Resource", new { resName = "View.html" });
        }

        public ActionResult Stats()
        {
            return this.Json(new
            {
                CPU = SelfPerfMonitor.GetCpuUsage(),
                RAM = SelfPerfMonitor.GetMemUsage()
            }, JsonRequestBehavior.AllowGet);
        }

        static Assembly assembly = typeof(WebStatsMonitorController).Assembly;
        static string namespacePrefix = typeof(WebStatsMonitorController).FullName.Replace(nameof(WebStatsMonitorController), "Res.");


        public ActionResult Resource(string resName)
        {
            var resStream = assembly.GetManifestResourceStream(namespacePrefix + resName);
            if (resStream == null) return new HttpNotFoundResult();
            byte[] content;
            using (var ms = new MemoryStream())
            {
                resStream.CopyTo(ms);
                content = ms.ToArray();
            }
            var extName = Path.GetExtension(resName).ToLower();
            switch(extName)
            {
                case ".html":
                case ".css":
                    var text = Encoding.UTF8.GetString(content);
                    text = text.Replace("~/", Request.ApplicationPath);
                    return Content(text, "text/" + extName.TrimStart('.'));
                case ".ttf":
                    return File(content, "application/font-sfnt");
                case ".js":
                    return File(content, "text/javascript");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}