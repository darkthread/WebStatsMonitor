using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DemoWebCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPostBusyLoop()
        {
            var timeout = DateTime.Now.AddSeconds(5);
            while (DateTime.Now.CompareTo(timeout) < 0)
            {
                var newGuid = Guid.NewGuid();
            }
        }

        static List<byte[]> MemoryMonster = new List<byte[]>();

        public void OnPostUseMemory()
        {
            byte[] buff = new byte[8 * 1024 * 1024];
            for (var i = 0; i < buff.Length; i++) buff[i] = (byte)(i % 256);
            MemoryMonster.Add(buff);
            //Sleep 5 seconds to keep the memory space "active"
            Thread.Sleep(5000);
        }
    }
}
