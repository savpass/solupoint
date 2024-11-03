using solupoint.Data;
using System;
using System.Collections.Generic;

public class PaymentCheck
{
    public int CustomerId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaymentValid { get; set; }
    public string PaymentStatus { get; set; }

    // Constructor
    public PaymentCheck(int customerId, decimal paymentAmount, DateTime paymentDate)
    {
        CustomerId = customerId;
        PaymentAmount = paymentAmount;
        PaymentDate = paymentDate;
        IsPaymentValid = false;
        PaymentStatus = "Pending";
    }

    // Validate the payment
    public bool ValidatePayment(decimal customerBalance)
    {
        // Check if payment amount is positive
        if (PaymentAmount <= 0)
        {
            PaymentStatus = "Invalid: Payment amount must be positive.";
            return false;
        }

        // Check if the customer has enough balance
        if (customerBalance < PaymentAmount)
        {
            PaymentStatus = "Failed: Insufficient balance.";
            return false;
        }

        // Update payment status and mark as valid
        IsPaymentValid = true;
        PaymentStatus = "Approved";
        return true;
    }

    // Process payment and update customer balance
    public bool ProcessPayment(ref decimal customerBalance)
    {
        if (!IsPaymentValid)
        {
            PaymentStatus = "Failed: Payment is not valid.";
            return false;
        }

        // Deduct payment amount from balance
        customerBalance -= PaymentAmount;
        PaymentStatus = "Completed";
        return true;
    }

    // Save this payment record to the JSON file
    public void SavePayment()
    {
        // Load existing payments from JSON
        List<PaymentCheck> payments = JsonDatabase.LoadPayments();

        // Add current payment to the list
        payments.Add(this);

        // Save the updated list back to JSON
        JsonDatabase.SavePayments(payments);
    }

    // Retrieve all payment records for a specific customer
    public static List<PaymentCheck> GetCustomerPayments(int customerId)
    {
        // Load all payments
        List<PaymentCheck> allPayments = JsonDatabase.LoadPayments();

        // Filter payments for the specified customer
        return allPayments.FindAll(p => p.CustomerId == customerId);
    }
}
