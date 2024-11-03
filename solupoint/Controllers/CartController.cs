//using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;


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

            // Customer details
            int customerId = 123;
            decimal paymentAmount = 200.00m;
            DateTime paymentDate = DateTime.Now;

            // Load customer's balance (in a real app, you would retrieve this from your data store)
            decimal customerBalance = 1000.00m;

            // Create a new payment check
            PaymentCheck payment = new PaymentCheck(customerId, paymentAmount, paymentDate);

            // Validate the payment
            if (payment.ValidatePayment(customerBalance))
            {
                // Process the payment if valid
                if (payment.ProcessPayment(ref customerBalance))
                {
                    // Save the payment record to JSON
                    payment.SavePayment();
                    Console.WriteLine("Payment processed and saved successfully.");
                }
                else
                {
                    Console.WriteLine($"Payment failed: {payment.PaymentStatus}");
                }
            }
            else
            {
                Console.WriteLine($"Payment validation failed: {payment.PaymentStatus}");
            }

            // Retrieve all payments for the customer
            List<PaymentCheck> customerPayments = PaymentCheck.GetCustomerPayments(customerId);



            Console.WriteLine($"Customer {customerId} has {customerPayments.Count} payments on record.");
            ViewBag.Message = $"Customer {customerId} has {customerPayments.Count} payments on record.";


            return View();
        }
    }
}


