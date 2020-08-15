using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BusyLoop()
        {
            var timeout = DateTime.Now.AddSeconds(5);
            while (DateTime.Now.CompareTo(timeout) < 0)
            {
                var newGuid = Guid.NewGuid();
            }
            return Content("OK");
        }

        static List<byte[]> MemoryMonster = new List<byte[]>();

        [HttpPost]
        public ActionResult UseMemory()
        {
            byte[] buff = new byte[8 * 1024 * 1024];
            for (var i = 0; i < buff.Length; i++) buff[i] = (byte)(i % 256);
            MemoryMonster.Add(buff);
            //Sleep 5 seconds to keep the memory space "active"
            Thread.Sleep(5000);
            return Content(MemoryMonster.Count().ToString());
        }

        
    }
}