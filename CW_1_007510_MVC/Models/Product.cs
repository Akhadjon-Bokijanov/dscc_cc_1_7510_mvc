using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CW_1_007510_MVC.Models
{
    public class Product
    {
        [Key]
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set;}
        public string Description { get; set; }
    }
}
