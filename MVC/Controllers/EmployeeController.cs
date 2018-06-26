using MVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {


        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<mvcEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
            return View(empList);
        }
        public ActionResult AddorEdit(int id=0)
        {
            if(id==0)
            return View(new mvcEmployeeModel());
            else
            {
                HttpResponseMessage respons = GlobalVariables.WebApiClient.GetAsync("Employee/"+id.ToString()).Result;
                return View(respons.Content.ReadAsAsync<mvcEmployeeModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(mvcEmployeeModel employee)
        {
            if (employee.EmployeeId == 0)
            {
                HttpResponseMessage respons = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", employee).Result;
                TempData["SuccessMessage"] = "Inserted New Row Successfully";
            }
            else {
                HttpResponseMessage respons = GlobalVariables.WebApiClient.PutAsJsonAsync("Employee/"+employee.EmployeeId,employee).Result;
                TempData["SuccessMessage"] = "Updated Row Successfully";
            }
            return RedirectToAction("Index");
           
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage respons = GlobalVariables.WebApiClient.DeleteAsync("Employee/" +id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted  Successfully";
            return RedirectToAction("Index");
        }
    }
}