using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DailyDairyAuto.Models;

namespace DailyDairyAuto.Controllers
{
    public class CattleDatumsController : Controller
    {
        private readonly DailyDairyContext _context;

        public CattleDatumsController(DailyDairyContext context)
        {
            _context = context;
        }

        // GET: CattleDatums
        public async Task<IActionResult> Index()
        {
              return _context.CattleData != null ? 
                          View(await _context.CattleData.ToListAsync()) :
                          Problem("Entity set 'DailyDairyContext.CattleData'  is null.");
        }

        // GET: CattleDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CattleData == null)
            {
                return NotFound();
            }

            var cattleDatum = await _context.CattleData
                .FirstOrDefaultAsync(m => m.GroupId == id);
           
            if (cattleDatum == null)
            {
                return NotFound();
            }

            
            return View(cattleDatum);
        }

        // GET: CattleDatums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CattleDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GrpAvgMilk,TotalCows")] CattleDatum cattleDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cattleDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cattleDatum);
        }

        // GET: CattleDatums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CattleData == null)
            {
                return NotFound();
            }

            var cattleDatum = await _context.CattleData.FindAsync(id);
            if (cattleDatum == null)
            {
                return NotFound();
            }
            return View(cattleDatum);
        }

        // POST: CattleDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GrpAvgMilk,TotalCows")] CattleDatum cattleDatum)
        {
            if (id != cattleDatum.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cattleDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CattleDatumExists(cattleDatum.GroupId))
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
            return View(cattleDatum);
        }

        // GET: CattleDatums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CattleData == null)
            {
                return NotFound();
            }

            var cattleDatum = await _context.CattleData
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (cattleDatum == null)
            {
                return NotFound();
            }

            return View(cattleDatum);
        }

        // POST: CattleDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CattleData == null)
            {
                return Problem("Entity set 'DailyDairyContext.CattleData'  is null.");
            }
            var cattleDatum = await _context.CattleData.FindAsync(id);
            if (cattleDatum != null)
            {
                _context.CattleData.Remove(cattleDatum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CattleDatumExists(int id)
        {
          return (_context.CattleData?.Any(e => e.GroupId == id)).GetValueOrDefault();
        }
    }
}
