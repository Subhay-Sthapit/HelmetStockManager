using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class HelmetSale
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Field Required.")]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "Field Required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSale { get; set; }

        // setting the foreign key connection 

        public virtual Client Client { get; set; }
    }
}
