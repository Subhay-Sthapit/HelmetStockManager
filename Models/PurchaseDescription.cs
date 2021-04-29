using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class PurchaseDescription
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Purchase")]
        public int PurchaseId { get; set; }
        [ForeignKey("Helmet")]
        public int HelmetId { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public int UnitPrice { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public int TotalAmount { get; set; }

        // making foreign key connections 
        public virtual Purchase Purchase { get; set; }

        public virtual Helmet Helmet { get; set; }
    }
}
