using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Data
{
    public class Award
    {
        [Key]
        public int Id { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
    }
}
