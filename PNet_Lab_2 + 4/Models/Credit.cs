using System;

namespace PNet_Lab_2.Models
{
    public class Credit
    {
        public int Id { get; set; }

        public int DebitorId { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public DateTime OpenDate { get; set; }
    }
}
