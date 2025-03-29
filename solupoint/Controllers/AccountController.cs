using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace solupoint.Controllers
{
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using System.IO;
    using solupoint.Models;

    public class Payment
    {
        public string CustomerId { get; set; }
       /// <summary>
       //public bool IsDue { get; set; }
       /// </summary>
        public string PaymentStatus { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentDate { get; set; }
    }


    public class AccountController : Controller
    {
        public ActionResult Pagamenti()
        {
            // Path to the JSON file
            string filePath = Server.MapPath("~/App_Data/duePayments.json");

            // Deserialize JSON file content to a list of payments
            List<Payment> payments = new List<Payment>();

            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                payments = JsonConvert.DeserializeObject<List<Payment>>(jsonContent);
            }

            // Check if there are due payments for the user
            var userPayments = payments?.FindAll(p => p.CustomerId != User.Identity.Name);

            // Pass the payments to the view
            return View(userPayments);
        }

        // Login action for displaying the login view
        public ActionResult Login()
        {
            return View();
        }

        // Register action for displaying the registration view
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            // Rimuovi l'utente dalla sessione
            Session.Remove("Username");

            // Reindirizza alla pagina di login
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var users = GetUsersFromJson(); // Carica gli utenti dal file JSON
                var user = users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Salva il nome utente nella sessione
                    Session["Username"] = user.Username;

                    // Reindirizza alla homepage o a un'area riservata
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Se username o password non sono validi, mostra un messaggio di errore
                    ModelState.AddModelError("", "Username o password non validi.");
                }
            }
            return View(model); // Torna alla pagina di login se ci sono errori
        }



        // POST: Handle registration
        [HttpPost]
        public ActionResult Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var users = GetUsersFromJson();
                if (users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");

                }
                else
                {
                    users.Add(new User { Username = model.Username, Password = model.Password });
                    SaveUsersToJson(users);
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        private List<User> GetUsersFromJson()
        {

            string _jsonFilePath = Server.MapPath("~/App_Data/Users.json");
            if (!System.IO.File.Exists(_jsonFilePath))
                return new List<User>();

            var json = System.IO.File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        private void SaveUsersToJson(List<User> users)
        {
            string _jsonFilePath = Server.MapPath("~/App_Data/Users.json");
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonFilePath, json);
        }
    }




}




