using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProgrammer.Api.Data.Entities;

namespace TestProgrammer.Api.Controllers.UI
{
    public class ProfilesController : Controller
    {
        private readonly Contexto _context;

        public ProfilesController(Contexto context)
        {
            _context = context;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profiles = await _context.Profiles
                .FirstOrDefaultAsync(m => m.ProfileID == id);
            if (profiles == null)
            {
                return NotFound();
            }

            return View(profiles);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileID,ProfileName")] Profiles profiles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profiles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profiles);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profiles = await _context.Profiles.FindAsync(id);
            if (profiles == null)
            {
                return NotFound();
            }
            return View(profiles);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileID,ProfileName")] Profiles profiles)
        {
            if (id != profiles.ProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfilesExists(profiles.ProfileID))
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
            return View(profiles);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profiles = await _context.Profiles
                .FirstOrDefaultAsync(m => m.ProfileID == id);
            if (profiles == null)
            {
                return NotFound();
            }

            return View(profiles);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profiles = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profiles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfilesExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileID == id);
        }
    }
}
