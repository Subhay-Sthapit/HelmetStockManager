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
    public class HelmetSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HelmetSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelmetSales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HelmetSale.Include(h => h.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HelmetSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetSale = await _context.HelmetSale
                .Include(h => h.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helmetSale == null)
            {
                return NotFound();
            }

            return View(helmetSale);
        }

        // GET: HelmetSales/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            return View();
        }

        // POST: HelmetSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,BillNumber,DateOfSale")] HelmetSale helmetSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(helmetSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Address", helmetSale.ClientId);
            return View(helmetSale);
        }

        // GET: HelmetSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetSale = await _context.HelmetSale.FindAsync(id);
            if (helmetSale == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Address", helmetSale.ClientId);
            return View(helmetSale);
        }

        // POST: HelmetSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,BillNumber,DateOfSale")] HelmetSale helmetSale)
        {
            if (id != helmetSale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helmetSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelmetSaleExists(helmetSale.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Address", helmetSale.ClientId);
            return View(helmetSale);
        }

        // GET: HelmetSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetSale = await _context.HelmetSale
                .Include(h => h.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helmetSale == null)
            {
                return NotFound();
            }

            return View(helmetSale);
        }

        // POST: HelmetSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helmetSale = await _context.HelmetSale.FindAsync(id);
            _context.HelmetSale.Remove(helmetSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelmetSaleExists(int id)
        {
            return _context.HelmetSale.Any(e => e.Id == id);
        }
    }
}
