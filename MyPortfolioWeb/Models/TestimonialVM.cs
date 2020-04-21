using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class TestimonialVM
    {
        public int Id { get; set; }
        [Display(Name="Full name")]
        [Required]
        public string Name { get; set; }
        [Display(Name="Testimonial")]
        [Required]
        public string Text { get; set; }
        [Display(Name="Date written")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name="Photo")]
        public IFormFile Img { get; set; }
    }
}
