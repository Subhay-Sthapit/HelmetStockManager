using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class HelmetSaleDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("HelmetSale")]
        public int HelmetSaleId { get; set; }
        [ForeignKey("Helmet")]
        public int HelmetId { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public int UnitPrice { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public int TotalAmount { get; set; }

        // making foreign key connections 
        public virtual HelmetSale HelmetSale { get; set; }

        public virtual Helmet Helmet { get; set; }
    }
}
