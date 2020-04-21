using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class ExperienceVM
    {
        public int Id { get; set; }
        [Display(Name="Company name")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Job title")]
        [Required]
        public string JobTitle { get; set; }
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }
        [Display(Name = "Is it your current job?")]
        public bool Current { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
    }
}
