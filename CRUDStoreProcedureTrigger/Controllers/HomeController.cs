using CRUDStoreProcedureTrigger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDStoreProcedureTrigger.Controllers
{
    public class HomeController : Controller
    {
        public CRUDModel CRUDModel;

        public HomeController()
        {
            CRUDModel = new CRUDModel();
        }
        public ActionResult Index()
        {
            Employee employee = new Employee()
            {
                Marks1 = 95,
                Marks2 = 94,
                Marks3 = 92,
                Marks4 = 90,
                Name = "Ankit"
            };
            CRUDModel.AddEmployee(employee);
            CRUDModel.GetAllEmployees();
            return View();
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