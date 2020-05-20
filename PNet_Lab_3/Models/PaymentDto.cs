using System;

namespace PNet_Lab_3.Models
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public int CreditId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}