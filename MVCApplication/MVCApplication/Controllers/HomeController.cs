using MVCApplication.Data;
using MVCApplication.Services;
using MVCApplication.Services.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new Repository();
            var entries = repo.GetEntries();
            var projects = repo.GetProjects();
            return View(BusinessHelper.CreateInvoiceViewModels(projects, entries));
        }

     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}