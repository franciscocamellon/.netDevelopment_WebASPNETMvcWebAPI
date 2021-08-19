using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;

namespace Presentation.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly MobileAppDbContext _context;

        public DeveloperController(MobileAppDbContext context)
        {
            _context = context;
        }

        // GET: Developer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Developers.ToListAsync());
        }

        // GET: Developer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerModel = await _context
                .Developers
                .Include(x => x.MobileApps)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (developerModel == null)
            {
                return NotFound();
            }

            return View(developerModel);
        }

        // GET: Developer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Developer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,GraduationDate,EmployedStatus,PublishedApps")] DeveloperModel developerModel)
        {
            if (ModelState.IsValid)
            {
                developerModel.Id = Guid.NewGuid();
                _context.Add(developerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(developerModel);
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerModel = await _context.Developers.FindAsync(id);
            if (developerModel == null)
            {
                return NotFound();
            }
            return View(developerModel);
        }

        // POST: Developer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,GraduationDate,EmployedStatus,PublishedApps")] DeveloperModel developerModel)
        {
            if (id != developerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(developerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeveloperModelExists(developerModel.Id))
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
            return View(developerModel);
        }

        // GET: Developer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerModel = await _context.Developers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (developerModel == null)
            {
                return NotFound();
            }

            return View(developerModel);
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var developerModel = await _context.Developers.FindAsync(id);
            _context.Developers.Remove(developerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeveloperModelExists(Guid id)
        {
            return _context.Developers.Any(e => e.Id == id);
        }
    }
}
