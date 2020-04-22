using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Data
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string DownloadLink { get; set; }
        public string LanguagesUsed { get; set; }
        public string GitLink { get; set; }
    }
}
