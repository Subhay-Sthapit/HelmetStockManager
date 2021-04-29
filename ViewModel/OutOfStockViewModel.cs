using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.ViewModel
{
    public class OutOfStockViewModel
    {
        public int HelmetId { get; set; }
        public string HelmetName { get; set; }
        public string HelmetCode { get; set; }
        public int HelmetQuantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StockedDate { get; set; }
        public string HelmetCategory { get; set; }
    }
}
