using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace solupoint.Data
{
    public class JsonDatabase
    {
        //
        private static readonly string PaymentFilePath = HttpContext.Current.Server.MapPath("~/App_Data/payments.json");

        // Load all payments from JSON file
        public static List<PaymentCheck> LoadPayments()
        {
            if (!File.Exists(PaymentFilePath))
            {
                return new List<PaymentCheck>(); // Return empty list if file doesn't exist
            }

            string jsonData = File.ReadAllText(PaymentFilePath);
            return JsonConvert.DeserializeObject<List<PaymentCheck>>(jsonData);
        }

        // Save all payments to JSON file
        public static void SavePayments(List<PaymentCheck> payments)
        {
            string jsonData = JsonConvert.SerializeObject(payments, Formatting.Indented);
            File.WriteAllText(PaymentFilePath, jsonData);
        }
    }
}







