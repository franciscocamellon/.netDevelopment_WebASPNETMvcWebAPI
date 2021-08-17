using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Models;

namespace Presentation.Controllers
{
    public class MobileAppController : Controller
    {
        private readonly AppLibraryDbContext _context;

        public MobileAppController(AppLibraryDbContext context)
        {
            _context = context;
        }

        // GET: MobileApp
        public async Task<IActionResult> Index()
        {
            return View(await _context.MobileAppModel.ToListAsync());
        }

        // GET: MobileApp/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAppModel = await _context.MobileAppModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobileAppModel == null)
            {
                return NotFound();
            }

            return View(mobileAppModel);
        }

        // GET: MobileApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MobileApp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PublishedStatus,PublishedDate,DevId")] MobileAppModel mobileAppModel)
        {
            if (ModelState.IsValid)
            {
                mobileAppModel.Id = Guid.NewGuid();
                _context.Add(mobileAppModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mobileAppModel);
        }

        // GET: MobileApp/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAppModel = await _context.MobileAppModel.FindAsync(id);
            if (mobileAppModel == null)
            {
                return NotFound();
            }
            return View(mobileAppModel);
        }

        // POST: MobileApp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,PublishedStatus,PublishedDate,DevId")] MobileAppModel mobileAppModel)
        {
            if (id != mobileAppModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobileAppModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileAppModelExists(mobileAppModel.Id))
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
            return View(mobileAppModel);
        }

        // GET: MobileApp/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAppModel = await _context.MobileAppModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobileAppModel == null)
            {
                return NotFound();
            }

            return View(mobileAppModel);
        }

        // POST: MobileApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mobileAppModel = await _context.MobileAppModel.FindAsync(id);
            _context.MobileAppModel.Remove(mobileAppModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobileAppModelExists(Guid id)
        {
            return _context.MobileAppModel.Any(e => e.Id == id);
        }
    }
}
