using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using POE_ST10152183.data;
using POE_ST10152183.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace POE_ST10152183.Controllers
{
    public class LoginController : Controller
    {
        public static string? role;
        DbLayer dbLayer;
        public LoginController(IConfiguration configuration)
        {
            dbLayer = new DbLayer(configuration);
        }
        public IActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Login(Users user)
        {

            List<int> Eid = new List<int>();
            List<int> Fid = new List<int>();
            List<int> Aid = new List<int>();
            Eid = dbLayer.GetEmployeeId();
            Fid = dbLayer.GetFarmerId();
            Aid = dbLayer.GetAdminId();
          
            if (user.usertype.Equals("admin"))
            {
                {
                    for (int i = 0; i < Aid.Count; i++)
                    {
                        AdminAccounts aa = dbLayer.GetAdmin(Aid[i]);
                        if (user.username != null && user.password != null) { 
                            if (user.username.Equals(aa.AdminUserName) && user.password.Equals(aa.AdminPassword))
                            {
                                HttpContext.Session.SetString("userType", "admin");
                                role = HttpContext.Session.GetString("userType");
                                return RedirectToAction("Index", "Employees");
                            }
                            else
                            {
                                ViewBag.Message = "Wrong admin username or password";

                            } 
                    }
                        else
                        {
                            ViewBag.Message = "admin username or password should not be empty";
                        }

                    }
                }
            }
            else if (user.usertype.Equals("employee"))
            {
                for (int i = 0; i < Eid.Count; i++)
                {
                    Employees em = dbLayer.GetEmployee(Eid[i]);
                    if(user.username!=null && user.password != null) { 
                    if (user.username.Equals(em.EUserName) && user.password.Equals(em.EPassword))
                    {
                        
                        HttpContext.Session.SetString("userType", "employee");
                        role = HttpContext.Session.GetString("userType");
                        return RedirectToAction("Index", "Farmers");
                    }
                    else
                    {
                        ViewBag.Message = "Wrong username or password";

                    }
                }
                        else
                {
                    ViewBag.Message = "admin username or password should not be empty";
                }
            }
            }
            else if (user.usertype.Equals("farmer"))
            {
                for (int i = 0; i < Fid.Count; i++)
                {
                    Farmers fa = dbLayer.GetFarmer(Fid[i]);
                    if (user.username != null && user.password != null) { 
                        if (user.username.Equals(fa.FUserName) && user.password.Equals(fa.FPassword))
                    {
                        HttpContext.Session.SetString("userType", "farmer");
                        HttpContext.Session.SetInt32("userId",(Fid[i]));
                        role = HttpContext.Session.GetString("userType");
                        return RedirectToAction("PIndex", "Farmers");
                    }
                    else
                    {
                        ViewBag.Message = "Wrong farmer username or password";

                    }
                    }
                    else
                    {
                        ViewBag.Message = "admin username or password should not be empty";
                    }
                }
            }
            else
            {
                return View();
            }
            return View();

        }
        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            
            return RedirectToAction("Login");
        }

    }
    }
