using System;
using System.Web.Mvc;

namespace solupoint.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Prodotti()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult CashPoint()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        public ActionResult Cart()
        {
            return View();
        }

        //[Route("privacy-policy")]
        //public ActionResult PrivacyPolicy()
        //{
        //    return View();
        //}



        // License Provisioning Endpoint
        [HttpPost]
        [Route("Home/ProvisionLicense")]
        public JsonResult ProvisionLicense(string clientId, int productId)
        {
            if (string.IsNullOrEmpty(clientId) || productId <= 0)
            {
                return Json(new { success = false, message = "Invalid request parameters." });
            }

            // Generate a unique license key
            string licenseKey = $"{clientId}-{productId}-{Guid.NewGuid()}";

            return Json(new { success = true, licenseKey = licenseKey });
        }
    }
}
