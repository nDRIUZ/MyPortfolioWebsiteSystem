using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class ContactVM
    {
        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name="Your e-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Message")]
        [Required]
        public string Text { get; set; }
    }
}
