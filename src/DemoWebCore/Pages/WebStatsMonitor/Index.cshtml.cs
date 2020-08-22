using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using DemoWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoWebCore.Pages.WebStatsMonitor
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