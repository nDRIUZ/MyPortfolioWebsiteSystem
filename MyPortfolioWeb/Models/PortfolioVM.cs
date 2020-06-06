using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class PortfolioVM
    {
        public int Id { get; set; }
        [Display(Name="Name of project")]
        [Required]
        public string ProjectName { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Image of project")]
        [Required]
        public IFormFile Img { get; set; }
        [Display(Name = "Link to the project")]
        [Required]
        public string DownloadLink { get; set; }
        [Display(Name = "Languages used")]
        [Required]
        public string LanguagesUsed { get; set; }
        [Display(Name = "Github link")]
        public string GitLink { get; set; }
    }
    public class PortfolioShowVM
    {
        public int Id { get; set; }
        [Display(Name = "Name of project")]
        [Required]
        public string ProjectName { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Image of project")]
        [Required]
        public string Img { get; set; }
        [Display(Name = "Link to the project")]
        [Required]
        public string DownloadLink { get; set; }
        [Display(Name = "Languages used")]
        [Required]
        public string LanguagesUsed { get; set; }
        [Display(Name = "Github link")]
        public string GitLink { get; set; }
    }
}
