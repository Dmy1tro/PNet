//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PNet_Lab_3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime PaymentDate { get; set; }
    
        public virtual Credit Credit { get; set; }
    }
}