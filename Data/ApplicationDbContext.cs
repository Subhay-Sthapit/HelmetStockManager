using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HelmetStockManager.Models;

namespace HelmetStockManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HelmetStockManager.Models.Category> Category { get; set; }
        public DbSet<HelmetStockManager.Models.Helmet> Helmet { get; set; }
        public DbSet<HelmetStockManager.Models.HelmetStock> HelmetStock { get; set; }
        public DbSet<HelmetStockManager.Models.Purchase> Purchase { get; set; }
        public DbSet<HelmetStockManager.Models.PurchaseDescription> PurchaseDescription { get; set; }
        public DbSet<HelmetStockManager.Models.Client> Client { get; set; }
        public DbSet<HelmetStockManager.Models.HelmetSale> HelmetSale { get; set; }
        public DbSet<HelmetStockManager.Models.HelmetSaleDetail> HelmetSaleDetail { get; set; }
    }
}
