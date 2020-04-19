using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class WelcomePageVM
    {
        public int Id { get; set; }
        [Display(Name="Background Image")]
        public IFormFile Img { get; set; }
        [Display(Name = "Your job title")]
        [Required]
        public string WelcomeWorkingTitle { get; set; }
        [Display(Name = "Short text about you")]
        [Required]
        public string WelcomeText { get; set; }
    }
}
