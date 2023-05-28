using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POE_ST10152183.data;
using POE_ST10152183.Models;
using POE_ST10152183.Controllers;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace POE_ST10152183.Controllers
{
    
    public class FarmersController : Controller
    {
        public static int Fid;
        DbLayer dblayer;
        public FarmersController(IConfiguration configuration)
        {
            dblayer = new DbLayer(configuration);
        }
        // GET: FarmersController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                List<Farmers> falist = dblayer.AllFarmers();
                return View(falist);
            }
            else
            {
                
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: FarmersController/Create
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

        // POST: FarmersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Farmers farmer) { 

            int x;
            try
            {
                x = dblayer.AddFarmers(farmer);
                if (x == 0)
                {
                    throw new Exception("Farmer not added");
    }
                return RedirectToAction(nameof(Index));
}
            catch (Exception ex)
            {
    ViewBag.Message = ex.Message;
    return View();
}
        }

        // GET: FarmersController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                return View(dblayer.GetFarmer(id));
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
        }

        // POST: FarmersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Farmers farmer)
        {
            int x;
            try
            {
                x = dblayer.UpdateFarmer(id, farmer);
                if (x == 0)
                {
                    throw new Exception("Farmer not updated");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: FarmersController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("userType") == "employee" || HttpContext.Session.GetString("userType") == "admin")
            {
                return View(dblayer.GetFarmer(id));
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
        }

        // POST: FarmersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            int x;
            try
            {
                x = dblayer.DeleteFarmer(id);
                if (x == 0)
                {
                    throw new Exception("Farmer not deleted");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public ActionResult PIndex()
        {

            if (HttpContext.Session.GetString("userType") =="farmer")
            {
                Fid = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
                List<Products> prlist = dblayer.FProducts(Fid);
                return View(prlist);
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
        }
        // GET: ProductsController/Create
        public ActionResult PCreate()
        {
            if (HttpContext.Session.GetString("userType") == "farmer")
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PCreate(Products product)
        {
            int x;
            Fid=Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            try
            {
               
                x = dblayer.AddProduct(product,Fid);
                if (x == 0)
                {
                    throw new Exception("Product not added");
                }
                return RedirectToAction(nameof(PIndex));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult PEdit(int id)
        {
            if (HttpContext.Session.GetString("userType") == "farmer")
            {
                return View(dblayer.GetProduct(id));
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
           
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PEdit(int id, Products product)
        {
            int x;
            Fid = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            try
            {
                x = dblayer.UpdateProduct(id, product,Fid);
                if (x == 0)
                {
                    throw new Exception("Product not updated");
                }
                return RedirectToAction(nameof(PIndex));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult PDelete(int id)
        {
            if (HttpContext.Session.GetString("userType") == "farmer")
            {
                return View(dblayer.GetProduct(id));
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PDelete(int id, IFormCollection collection)
        {
            int x;
            try
            {
                x = dblayer.DeleteProduct(id);
                if (x == 0)
                {
                    throw new Exception("Product not deleted");
                }
                return RedirectToAction(nameof(PIndex));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
