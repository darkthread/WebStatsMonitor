using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebStatsMonitorCore.Areas.WebStatsMonitor.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnGetStats()
        {
            return new JsonResult(new
            {
                CPU = SelfPerfMonitor.GetCpuUsage(),
                RAM = SelfPerfMonitor.GetMemUsage()
            }, new System.Text.Json.JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                Encoder =
                    JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            });
        }
    }
}

