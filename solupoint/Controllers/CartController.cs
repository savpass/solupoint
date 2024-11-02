//using Microsoft.Data.Sqlite;
using System;
using System.Data.SQLite;
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
            string connectionString = System.Configuration.ConfigurationManager
                         .ConnectionStrings["SQLiteConnection"].ConnectionString;

            //string connectionString = "Data Source=|DataDirectory|/mydatabase.db";

            // Example of interacting with SQLite database
            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Example SQL to insert or query data
                    string query = "INSERT INTO Orders (OrderDate, OrderAmount) VALUES (@OrderDate, @OrderAmount);";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        command.Parameters.AddWithValue("@OrderAmount", 100.00); // Example amount

                        command.ExecuteNonQuery(); // Execute the insert
                    }

                    ViewBag.Message = V; // Set a success message
                }
                catch (Exception ex)
                {
                    // Log the exception and set an error message
                    ViewBag.Message = "Error: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

            // Return to the Index view


            return View();
        }
    }
}


