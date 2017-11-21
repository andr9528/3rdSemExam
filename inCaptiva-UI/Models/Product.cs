using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.Models {
    public class Product {
        public string Name {
            get; set;
        }
        public string Category {
            get; set;
        }
        public decimal Price {
            get; set;
        }
    }
}