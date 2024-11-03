//using Microsoft.Data.Sqlite;
using System;
using System.Web.Mvc;


namespace solupoint.Controllers
{
    public class CartController : Controller
    {
        private const string V = "Checkout successful!";

        // GET: Cart

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }

        // This action will be called when the button is clicked
        [HttpPost]
        public ActionResult Checkout()
        {

            ViewBag.Message = V;
            return View();
        }
    }
}


