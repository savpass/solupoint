using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Net.Mail;

namespace solupoint.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faq()
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

        public ActionResult Costi()
        {
            return View();
        }



        [HttpPost]
        public ActionResult SendContact(string name, string email, string message)
        {
            var mail = new MailMessage();
            mail.To.Add("info@solupoint.com");
            mail.From = new MailAddress(email);
            mail.Subject = "Nuovo messaggio da " + name;
            mail.Body = message;

            var smtp = new SmtpClient("smtp.aruba.it", 587)
            {
                Credentials = new System.Net.NetworkCredential("info@solupoint.com", "Cashpoint123!!"),
                EnableSsl = true
            };

             smtp.Send(mail);

            TempData["Message"] = "Messaggio inviato con successo!";
            return RedirectToAction("Index");
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
