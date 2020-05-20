using System;

namespace PNet_Lab_2.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int CreditId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
