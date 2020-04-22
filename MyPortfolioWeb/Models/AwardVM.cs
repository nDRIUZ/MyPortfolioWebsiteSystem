using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class AwardVM
    {
        public int Id { get; set; }
        [Display(Name="Image of Award/Certification")]
        [Required]
        public IFormFile Img { get; set; }
        [Display(Name="Name of award/certification")]
        [Required]
        public string Name { get; set; }
        [Display(Name= "Date achieved")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
        [Display(Name="Link to award/certification website")]
        public string Link { get; set; }
    }
}
