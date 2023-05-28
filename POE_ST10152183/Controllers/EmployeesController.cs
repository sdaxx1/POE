using Microsoft.AspNetCore.Mvc;
using System.Data;
using POE_ST10152183.Models;
using POE_ST10152183.data;
using System;

namespace POE_ST10152183.Controllers
{

    public class EmployeesController : Controller
    {
        DbLayer dblayer;
        public EmployeesController(IConfiguration configuration)
        {
            dblayer = new DbLayer(configuration);
        }
        //GET: EmployeesController

        
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType")=="admin") 
            {
                List<Employees> emlist = dblayer.AllEmployees();
                return View(emlist);
            }
            else 
            {
               
                return RedirectToAction("Login", "Login");
            }

        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                return View();
            }
            else
            {
               
                return RedirectToAction("Login", "Login");
            }
            
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees employee)
        {
            int x;
            try
            {
                x = dblayer.Addemployee(employee);
                if (x == 0)
                {
                    throw new Exception("employee not added");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                return View(dblayer.GetEmployee(id));
            }
            else
            {              
                return RedirectToAction("Login", "Login");
            }
            
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employees employee)
        {
            int x;
            try
            {
                x = dblayer.UpdateEmployee(id,employee);
                if (x == 0)
                {
                    throw new Exception("Employee not updated");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                return View(dblayer.GetEmployee(id));
            }
            else
            {
               
                return RedirectToAction("Login", "Login");
            }
         
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            int x;
            try
            {
                x = dblayer.DeleteEmployee(id);
                if (x == 0)
                {
                    throw new Exception("Employee not deleted");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        
        public ActionResult AllPIndex(string searchBy,string? searchValue,DateTime? start,DateTime? end)
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                List<Products> prlist = dblayer.AllProducts();
                if (!string.IsNullOrEmpty(searchBy))
                {
                    if (searchBy == "ProductType" &&searchValue!=null)
                    {

                        return View(prlist.Where(p => p.ProductType.Contains(searchValue)));
                    }
                    else if (searchBy == "FarmerId" && searchValue != null)
                    {
                        var searchByFI = prlist.Where(p => p.FarmerId== int.Parse(searchValue));
                        return View(searchByFI);
                    }
                    else if (searchBy == "UploadDate")
                    {
                        var searchByUP = prlist.Where(p => (start <= p.UploadDate && p.UploadDate <= end));
                        return View(searchByUP);
                    }
                    else
                    {
                        return View(prlist);
                    }
                }
                else
                {
                    return View(prlist);
                }
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
          
        }
        
    }
}

