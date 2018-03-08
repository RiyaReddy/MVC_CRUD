using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string PName { get; set; }
        public Nullable<double> Price { get; set; }
    }
}