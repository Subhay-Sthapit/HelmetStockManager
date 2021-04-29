using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field Required.")]
        public string Email { get; set; }
    }
}
