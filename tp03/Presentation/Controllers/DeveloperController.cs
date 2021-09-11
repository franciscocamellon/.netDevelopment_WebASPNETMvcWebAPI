using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IDeveloperHttpService _developerHttpService;

        public DeveloperController(
            IDeveloperHttpService developerHttpService)
        {
            _developerHttpService = developerHttpService;
        }

        // GET: Developer
        public async Task<IActionResult> Index(DeveloperIndexViewModel developerIndexRequest)
        {
            var developerIndexViewModel = new DeveloperIndexViewModel
            {
                Search = developerIndexRequest.Search,
                OrderAscendant = developerIndexRequest.OrderAscendant,
                Developers = await _developerHttpService.GetAllAsync(
                    developerIndexRequest.OrderAscendant,
                    developerIndexRequest.Search)
            };
            return View(developerIndexViewModel);
        }

        // GET: Developer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerViewModel = await _developerHttpService.GetByIdAsync(id.Value);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
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
        public async Task<IActionResult> Create(DeveloperViewModel developerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(developerViewModel);
            }
            
            var developerCreated = await _developerHttpService.CreateAsync(developerViewModel);

            return RedirectToAction(nameof(Details), new {id = developerCreated.Id});
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerViewModel = await _developerHttpService.GetByIdAsync(id.Value);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        // POST: Developer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DeveloperViewModel developerViewModel)
        {
            if (id != developerViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(developerViewModel);
            }
            
            try
            {
                await _developerHttpService.EditAsync(developerViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await developerViewModelExistsAsync(developerViewModel.Id);

                if (!exists)
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

        // GET: Developer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerViewModel = await _developerHttpService.GetByIdAsync(id.Value);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _developerHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> developerViewModelExistsAsync(Guid id)
        {
            var developer = await _developerHttpService.GetByIdAsync(id);

            var any = developer != null;

            return any;
        }
    }
}
