using HelmetStockManager.Data;
using HelmetStockManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmetStockManager.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // number 5.6 displays the details of the helmets in stock. Helmet. Category. HelmetStock Search by Helmet Name
        // HelmetStockViewModel
        public IActionResult HelmetStockListReport(string search)
        {
            List<HelmetStockViewModel> listData = new List<HelmetStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT h.id as HelmetId, HelmetCode, HelmetName, Quantity as Quantity, c.Name as HelmetCategory from Helmet h inner join HelmetStock s on h.id=s.HelmetId join Category c on c.id=h.Category_id";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    HelmetStockViewModel data;
                    while (result.Read())
                    {
                        data = new HelmetStockViewModel();
                        data.HelmetId = result.GetInt32(0);
                        data.HelmetCode = result.GetString(1);
                        data.HelmetName = result.GetString(2);
                        data.Quantity = result.GetInt32(3);
                        data.HelmetCategory = result.GetString(4);
                        listData.Add(data);
                    }
                }
            }
            var a = listData.Where(x => x.Quantity > 0);
            if (search != null)
            {
                return View(listData.Where(x => x.HelmetName == search && x.Quantity > 0));
            }
            else
            {
                return View(listData.Where(x => x.Quantity > 0));
            }
        }

        // Number 8 Client Sale Details of the last 31 days.
        // Client. HelmetSale. HelmetSaleDetail. #Search by Client Name Uses ClientSaleVeiwModel
        public IActionResult ClientSaleReport(string search)
        {
            List<ClientSaleViewModel> listData = new List<ClientSaleViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT c.id as ClientId, Name, DateOfSale, sd.Quantity as SaleQuantity, sd.UnitPrice, TotalAmount, HelmetName from Client c join HelmetSale s on c.id=s.ClientId join HelmetSaleDetail sd on sd.HelmetSaleId=s.id join Helmet i on sd.HelmetId=i.id";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    ClientSaleViewModel data;
                    while (result.Read())
                    {
                        data = new ClientSaleViewModel();
                        data.ClientId = result.GetInt32(0);
                        data.ClientName = result.GetString(1);
                        data.DateOfSale = result.GetDateTime(2);
                        data.SaleQuantity = result.GetInt32(3);
                        data.UnitPrice = result.GetInt32(4);
                        data.TotalAmount = result.GetInt32(5);
                        data.HelmetName = result.GetString(6);
                        listData.Add(data);
                    }
                }
            }
            DateTime dateNow = DateTime.Now;
            TimeSpan aMonth = new TimeSpan(31, 0, 0, 0);
            DateTime monthBefore = dateNow.Subtract(aMonth);
            return View(listData.Where(x => x.ClientName == search && x.DateOfSale > monthBefore));
        }

        // Number 9. Dispaly Helmet Details if HelmeStock < 10. Helmet. Category. HelmetStock. LowStockViewModel
        public IActionResult LowStockReport()
        {
            List<LowStockViewModel> listData = new List<LowStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT h.id as HelmetId, HelmetName, HelmetCode, st.Quantity as HelmetQuantity, c.Name as HelmetCategory from Helmet h join HelmetStock st on h.id=st.HelmetId join Category c on c.id=h.Category_id";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    LowStockViewModel data;
                    while (result.Read())
                    {
                        data = new LowStockViewModel();
                        data.HelmetId = result.GetInt32(0);
                        data.HelmetName = result.GetString(1);
                        data.HelmetCode = result.GetString(2);
                        data.HelmetQuantity = result.GetInt32(3);
                        data.HelmetCategory = result.GetString(4);
                        listData.Add(data);
                    }
                }
            }
            return View(listData.Where(x => x.HelmetQuantity < 10 && x.HelmetQuantity != 0));
        }

        // 11. Displays Details of Helmets that are out of stock. Helmet. Category. HelmetStock. Purchase. PurchaseDescription OutOfStockViewModel
        public IActionResult OutOfStockReport(string option)
        {
            List<OutOfStockViewModel> listData = new List<OutOfStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT h.Id as HelmetId, HelmetName, HelmetCode, st.Quantity as HelmetQuantity, DateOfPurchase as StockedDate, c.Name as HelmetCategory from Helmet h join HelmetStock st on h.Id=st.HelmetId join PurchaseDescription pd on h.id=pd.HelmetId join Purchase p on pd.PurchaseId=p.Id join Category c on h.Category_Id=c.id";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    OutOfStockViewModel data;
                    while (result.Read())
                    {
                        data = new OutOfStockViewModel();
                        data.HelmetId = result.GetInt32(0);
                        data.HelmetName = result.GetString(1);
                        data.HelmetCode = result.GetString(2);
                        data.HelmetQuantity = result.GetInt32(3);
                        data.StockedDate = result.GetDateTime(4);
                        data.HelmetCategory = result.GetString(5);
                        listData.Add(data);
                    }
                }
            }
            if (option == "Date")
            {
                var d = listData.Where(x => x.HelmetQuantity == 0);
                return View(d.OrderByDescending(x => x.StockedDate));
            }
            else if (option == "Name")
            {
                var a = listData.Where(x => x.HelmetQuantity == 0);
                return View(a.OrderBy(x => x.HelmetName));
            }
            else
            {
                return View(listData.Where(x => x.HelmetQuantity == 0));
            }
        }


        // 12 displays details of client who have been inactive for more than 31 days. Client. HelmetSale
        public IActionResult InactiveClientReport()
        {
            List<InactiveClient> listData = new List<InactiveClient>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT c.id as ClientId, Name as ClientName, Email as ClientEmail, MAX(DateOfSale) as DateOfLastSale from Client c join HelmetSale s on c.id=s.ClientId group by Name, c.id, Email";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    InactiveClient data;
                    while (result.Read())
                    {
                        data = new InactiveClient();
                        data.ClientId = result.GetInt32(0);
                        data.ClientName = result.GetString(1);
                        data.ClientEmail = result.GetString(2);
                        data.DateOfLastSale = result.GetDateTime(3);
                        listData.Add(data);
                    }
                }
            }
            DateTime dateNow = DateTime.Now;
            TimeSpan aMonth = new TimeSpan(31, 0, 0, 0);
            DateTime monthBefore = dateNow.Subtract(aMonth);
            return View(listData.Where(x => x.DateOfLastSale < monthBefore));
        }

        // Displays helmets that have not been sold in the last 31 days.
        // Helmet. Helmetstock. HelmetSaleDetail. HelmetSale
        public IActionResult UnsoldHelmetReport()
        {
            List<UnsoldHelmetsViewModel> listData = new List<UnsoldHelmetsViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT h.id as HelmetId, HelmetName, HelmetCode, MAX(st.Quantity) as HelmetQuantity, MAX(sa.DateOfSale) as DateOfLastSale from Helmet h join HelmetStock st on h.id=st.HelmetId join HelmetSaleDetail sd on h.id=sd.HelmetId join HelmetSale sa on sa.id=sd.HelmetsaleId group by h.id, HelmetName, HelmetCode";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    UnsoldHelmetsViewModel data;
                    while (result.Read())
                    {
                        data = new UnsoldHelmetsViewModel();
                        data.HelmetId = result.GetInt32(0);
                        data.HelmetName = result.GetString(1);
                        data.HelmetCode = result.GetString(2);
                        data.HelmetQuantity = result.GetInt32(3);
                        data.DateOfLastSale = result.GetDateTime(4);
                        listData.Add(data);
                    }
                }
            }
            DateTime dateNow = DateTime.Now;
            TimeSpan aMonth = new TimeSpan(31, 0, 0, 0);
            DateTime monthBefore = dateNow.Subtract(aMonth);
            return View(listData.Where(x => x.DateOfLastSale < monthBefore));
        }


    }
}
