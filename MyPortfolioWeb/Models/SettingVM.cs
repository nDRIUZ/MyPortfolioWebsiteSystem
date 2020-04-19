using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Models
{
    public class SettingVM
    {
        public int Id { get; set; }
        [Display(Name="Facebook account link")]
        public string FacebookLink { get; set; }
        [Display(Name = "Instagram account link")]
        public string InstaLink { get; set; }
        [Display(Name = "LinkedIn account link")]
        public string LinkedInLink { get; set; }
        [Display(Name = "Twitter account link")]
        public string TwitterLink { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        [Display(Name = "Link to your resume")]
        public string ResumeLink { get; set; }
    }
}
