using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PNet_Lab_3.Models
{
    public class DebitorDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string PostNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
    }
}