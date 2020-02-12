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
    public class EmployeesController : Controller
    {
        private readonly Contexto _context;

        public EmployeesController(Contexto context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Employees.Include(e => e.Positions).Include(e => e.Profiles);
            return View(await contexto.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Positions)
                .Include(e => e.Profiles)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "PositionID");
            ViewData["ProfileID"] = new SelectList(_context.Profiles, "ProfileID", "ProfileID");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,EmployeeName,PositionID,ProfileID")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "PositionID", employees.PositionID);
            ViewData["ProfileID"] = new SelectList(_context.Profiles, "ProfileID", "ProfileID", employees.ProfileID);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "PositionID", employees.PositionID);
            ViewData["ProfileID"] = new SelectList(_context.Profiles, "ProfileID", "ProfileID", employees.ProfileID);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,EmployeeName,PositionID,ProfileID")] Employees employees)
        {
            if (id != employees.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeID))
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
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "PositionID", employees.PositionID);
            ViewData["ProfileID"] = new SelectList(_context.Profiles, "ProfileID", "ProfileID", employees.ProfileID);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Positions)
                .Include(e => e.Profiles)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
