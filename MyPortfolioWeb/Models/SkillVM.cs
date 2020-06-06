using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class SkillVM
    { 
        public int Id { get; set; }
        [Required]
        [Display(Name="Name of the skill")]
        public string SkillName { get; set; }
        [Required]
        [Display(Name = "Skill picture")]
        public IFormFile SkillImg { get; set; }
    }

    public class SkillShowVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name of the skill")]
        public string SkillName { get; set; }
        [Required]
        [Display(Name = "Skill picture")]
        public string SkillImg { get; set; }
    }
}
