using System;
using System.Web.Mvc;

namespace solupoint.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        
        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
