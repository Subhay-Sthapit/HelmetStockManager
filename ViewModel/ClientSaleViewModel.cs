using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.ViewModel
{
    public class ClientSaleViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSale { get; set; }
        public int SaleQuantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalAmount { get; set; }
        public string HelmetName { get; set; }
    }
}
