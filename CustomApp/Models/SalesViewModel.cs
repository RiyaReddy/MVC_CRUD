using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomApp.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }
        
        public Nullable<System.DateTime> DateSold { get; set; }
        public string CName { get; set; }
        public string PName { get; set; }
        public string SName { get; set; }
    }
}