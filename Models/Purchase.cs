using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public string Seller { get; set; }

        [Required(ErrorMessage = "Field Required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPurchase { get; set; }

        [Required(ErrorMessage = "Field Required.")]
        public string BillNumber { get; set; }
    }
}
