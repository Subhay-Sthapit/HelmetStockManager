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
    public class HelmetStocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HelmetStocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelmetStocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HelmetStock.Include(h => h.Helmet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HelmetStocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetStock = await _context.HelmetStock
                .Include(h => h.Helmet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helmetStock == null)
            {
                return NotFound();
            }

            return View(helmetStock);
        }

        // GET: HelmetStocks/Create
        public IActionResult Create()
        {
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetName");
            return View();
        }

        // POST: HelmetStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HelmetId,Quantity")] HelmetStock helmetStock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(helmetStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", helmetStock.HelmetId);
            return View(helmetStock);
        }

        // GET: HelmetStocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetStock = await _context.HelmetStock.FindAsync(id);
            if (helmetStock == null)
            {
                return NotFound();
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", helmetStock.HelmetId);
            return View(helmetStock);
        }

        // POST: HelmetStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HelmetId,Quantity")] HelmetStock helmetStock)
        {
            if (id != helmetStock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helmetStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelmetStockExists(helmetStock.Id))
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
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", helmetStock.HelmetId);
            return View(helmetStock);
        }

        // GET: HelmetStocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetStock = await _context.HelmetStock
                .Include(h => h.Helmet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helmetStock == null)
            {
                return NotFound();
            }

            return View(helmetStock);
        }

        // POST: HelmetStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helmetStock = await _context.HelmetStock.FindAsync(id);
            _context.HelmetStock.Remove(helmetStock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelmetStockExists(int id)
        {
            return _context.HelmetStock.Any(e => e.Id == id);
        }
    }
}
