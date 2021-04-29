using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class Helmet
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public string HelmetName { get; set; }

        [Required(ErrorMessage = "Field Required.")]
        public string HelmetCode { get; set; }
        public string Descripiton { get; set; }

        // addin foreign key link 
        public virtual Category Category { get; set; }
    }
}
