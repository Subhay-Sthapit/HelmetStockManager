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
    public class HelmetSaleDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HelmetSaleDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelmetSaleDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HelmetSaleDetail.Include(h => h.Helmet).Include(h => h.HelmetSale);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HelmetSaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetSaleDetail = await _context.HelmetSaleDetail
                .Include(h => h.Helmet)
                .Include(h => h.HelmetSale)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helmetSaleDetail == null)
            {
                return NotFound();
            }

            return View(helmetSaleDetail);
        }

        // GET: HelmetSaleDetails/Create
        public IActionResult Create()
        {
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetName");
            ViewData["HelmetSaleId"] = new SelectList(_context.HelmetSale, "Id", "BillNumber");
            return View();
        }

        // POST: HelmetSaleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HelmetSaleId,HelmetId,Quantity,UnitPrice,TotalAmount")] HelmetSaleDetail helmetSaleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(helmetSaleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", helmetSaleDetail.HelmetId);
            ViewData["HelmetSaleId"] = new SelectList(_context.HelmetSale, "Id", "BillNumber", helmetSaleDetail.HelmetSaleId);
            return View(helmetSaleDetail);
        }

        // GET: HelmetSaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetSaleDetail = await _context.HelmetSaleDetail.FindAsync(id);
            if (helmetSaleDetail == null)
            {
                return NotFound();
            }
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", helmetSaleDetail.HelmetId);
            ViewData["HelmetSaleId"] = new SelectList(_context.HelmetSale, "Id", "BillNumber", helmetSaleDetail.HelmetSaleId);
            return View(helmetSaleDetail);
        }

        // POST: HelmetSaleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HelmetSaleId,HelmetId,Quantity,UnitPrice,TotalAmount")] HelmetSaleDetail helmetSaleDetail)
        {
            if (id != helmetSaleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helmetSaleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelmetSaleDetailExists(helmetSaleDetail.Id))
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
            ViewData["HelmetId"] = new SelectList(_context.Helmet, "Id", "HelmetCode", helmetSaleDetail.HelmetId);
            ViewData["HelmetSaleId"] = new SelectList(_context.HelmetSale, "Id", "BillNumber", helmetSaleDetail.HelmetSaleId);
            return View(helmetSaleDetail);
        }

        // GET: HelmetSaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helmetSaleDetail = await _context.HelmetSaleDetail
                .Include(h => h.Helmet)
                .Include(h => h.HelmetSale)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helmetSaleDetail == null)
            {
                return NotFound();
            }

            return View(helmetSaleDetail);
        }

        // POST: HelmetSaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helmetSaleDetail = await _context.HelmetSaleDetail.FindAsync(id);
            _context.HelmetSaleDetail.Remove(helmetSaleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelmetSaleDetailExists(int id)
        {
            return _context.HelmetSaleDetail.Any(e => e.Id == id);
        }
    }
}
