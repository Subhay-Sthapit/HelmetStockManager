using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class HelmetStock
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Helmet")]
        public int HelmetId { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public int Quantity { get; set; }

        // setting foreign key link
        public virtual Helmet Helmet { get; set; }
    }
}
