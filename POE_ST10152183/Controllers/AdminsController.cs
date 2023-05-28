using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POE_ST10152183.Models;
using System.Drawing.Text;

namespace POE_ST10152183.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
