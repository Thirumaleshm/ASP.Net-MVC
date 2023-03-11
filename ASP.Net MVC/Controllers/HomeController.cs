using ASP.Net_MVC.Models;
using CRUDOPMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRUDOPMVC.Controllers
{
    public class HomeController : Controller
    {
        CompanyDBContext dbDataContext = new CompanyDBContext();
        public ActionResult Index()
        {
            List<Employee> employees = dbDataContext.GetEmployees();
            return View(employees);
        }
        public ActionResult RegisterEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterEmp(Employee emp)
        {
            dbDataContext.InsertEmp(emp);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            Employee emp = dbDataContext.GetEmployee(id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            dbDataContext.UpdateEmp(emp);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete(int id)
        {
            dbDataContext.DeleteEmp(id);
            return RedirectToAction("Index", "Home");
        }
    }
}