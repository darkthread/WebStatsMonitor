using DemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Controllers
{
    public class WebStatsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stats()
        {
            return this.Json(new
            {
                CPU = SelfPerfMonitor.GetCpuUsage(),
                RAM = SelfPerfMonitor.GetMemUsage()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}