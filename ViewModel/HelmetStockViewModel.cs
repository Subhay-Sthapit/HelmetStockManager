using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.ViewModel
{
    public class HelmetStockViewModel
    {
        public int HelmetId { get; set; }
        public string HelmetName { get; set; }
        public string HelmetCode { get; set; }
        public string Description { get; set; }
        public string HelmetCategory { get; set; }
        public int Quantity { get; set; }
    }
}
