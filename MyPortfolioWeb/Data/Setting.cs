using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Data
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string FacebookLink { get; set; }
        public string InstaLink { get; set; }
        public string LinkedInLink { get; set; }
        public string TwitterLink { get; set; }
        public string FullName { get; set; }
        public string ResumeLink { get; set; }
    }
}
