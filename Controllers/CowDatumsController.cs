using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DailyDairyAuto.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DailyDairyAuto.Controllers
{
    public class CowDatumsController : Controller
    {
        private readonly DailyDairyContext _context;

        public CowDatumsController(DailyDairyContext context)
        {
            _context = context;
        }

        // GET: CowDatums
        public async Task<IActionResult> Index()
        {
            var dailyDairyContext = _context.CowData.Include(c => c.Group);
            return View(await dailyDairyContext.ToListAsync());
        }

        // GET: CowDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CowData == null)
            {
                return NotFound();
            }

            var cowDatum = await _context.CowData.Include(c => c.Group).FirstOrDefaultAsync(m => m.CowId == id);
            var lactation = await _context.lactationstage.FirstOrDefaultAsync(c => c.CowId == id);
            if (cowDatum == null)
            {
                return NotFound();
            }
            if (lactation == null)
            {
                return NotFound();
            }
            var models = new Tuple<CowDatum, Lactation>(cowDatum, lactation);
            return View(models);

           
        }

        // GET: CowDatums/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.CattleData, "GroupId", "GroupId");
            return View();
        }

        // POST: CowDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Age,Breed,Status,Vaccinated,AvgMilkProd,GroupId")] CowDatum cowDatum)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState is valid");
                _context.Add(cowDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            Debug.WriteLine(errors);

            ViewData["GroupId"] = new SelectList(_context.CattleData, "GroupId", "GroupId", cowDatum.GroupId);
            return View(cowDatum);
        }

        // GET: CowDatums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CowData == null)
            {
                return NotFound();
            }

            var cowDatum = await _context.CowData.FindAsync(id);
            if (cowDatum == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.CattleData, "GroupId", "GroupId", cowDatum.GroupId);
            return View(cowDatum);
        }

        // POST: CowDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CowId,Age,Breed,Status,Vaccinated,AvgMilkProd,GroupId")] CowDatum cowDatum)
        {
            if (id != cowDatum.CowId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(cowDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CowDatumExists(cowDatum.CowId))
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
            ViewData["GroupId"] = new SelectList(_context.CattleData, "GroupId", "GroupId", cowDatum.GroupId);
            return View(cowDatum);
        }

        // GET: CowDatums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CowData == null)
            {
                return NotFound();
            }

            var cowDatum = await _context.CowData
                .Include(c => c.Group)
                .FirstOrDefaultAsync(m => m.CowId == id);
            if (cowDatum == null)
            {
                return NotFound();
            }

            return View(cowDatum);
        }

        // POST: CowDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CowData == null)
            {
                return Problem("Entity set 'DailyDairyContext.CowData'  is null.");
            }
            var cowDatum = await _context.CowData.FindAsync(id);
            if (cowDatum != null)
            {
                _context.CowData.Remove(cowDatum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CowDatumExists(int id)
        {
          return (_context.CowData?.Any(e => e.CowId == id)).GetValueOrDefault();
        }
    }
}
