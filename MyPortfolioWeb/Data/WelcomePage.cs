using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Data
{
    public class WelcomePage
    {
        [Key]
        public int Id { get; set; }
        public string Img { get; set; }
        public string WelcomeWorkingTitle { get; set; }
        public string WelcomeText { get; set; }
    }
}
