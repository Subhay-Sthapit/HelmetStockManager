using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelmetStockManager.Data;
using HelmetStockManager.Models;

namespace HelmetStockManager.Controllers
{
    public class PurchaseDescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseDescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseDescriptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseDescription.Include(p => p.Helmet).Include(p => p.Purchase);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseDescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDescription = await _context.PurchaseDescription
                .Include(p => p.Helmet)
                .Include(p => p.Purchase)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseDescription == null)
            {
                return NotFound();
            }

            return View(purchaseDescription);
        }

        // GET: PurchaseDescriptions/Create
        public IActionResult Create()
        {
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetName");
            ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "BillNumber");
            return View();
        }

        // POST: PurchaseDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PurchaseId,HelmetId,Quantity,UnitPrice,TotalAmount")] PurchaseDescription purchaseDescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", purchaseDescription.HelmetId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "BillNumber", purchaseDescription.PurchaseId);
            return View(purchaseDescription);
        }

        // GET: PurchaseDescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDescription = await _context.PurchaseDescription.FindAsync(id);
            if (purchaseDescription == null)
            {
                return NotFound();
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", purchaseDescription.HelmetId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "BillNumber", purchaseDescription.PurchaseId);
            return View(purchaseDescription);
        }

        // POST: PurchaseDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PurchaseId,HelmetId,Quantity,UnitPrice,TotalAmount")] PurchaseDescription purchaseDescription)
        {
            if (id != purchaseDescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDescriptionExists(purchaseDescription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", purchaseDescription.HelmetId);
            ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "BillNumber", purchaseDescription.PurchaseId);
            return View(purchaseDescription);
        }

        // GET: PurchaseDescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDescription = await _context.PurchaseDescription
                .Include(p => p.Helmet)
                .Include(p => p.Purchase)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseDescription == null)
            {
                return NotFound();
            }

            return View(purchaseDescription);
        }

        // POST: PurchaseDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseDescription = await _context.PurchaseDescription.FindAsync(id);
            _context.PurchaseDescription.Remove(purchaseDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseDescriptionExists(int id)
        {
            return _context.PurchaseDescription.Any(e => e.Id == id);
        }
    }
}
