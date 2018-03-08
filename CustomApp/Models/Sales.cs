using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomApp.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public Nullable<System.DateTime> DateSold { get; set; }

        public string CName { get; set; }
        public string PName { get; set; }
        public string SName { get; set; }
    }
}