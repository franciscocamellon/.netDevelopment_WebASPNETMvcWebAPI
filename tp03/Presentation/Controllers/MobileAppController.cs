using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers
{
    public class MobileAppController : Controller
    {
        private readonly IMobileAppHttpService _mobileAppHttpService;
        private readonly IDeveloperHttpService _developerHttpService;

        public MobileAppController(
            IMobileAppHttpService mobileAppHttpService,
            IDeveloperHttpService developerHttpService)
        {
            _mobileAppHttpService = mobileAppHttpService;
            _developerHttpService = developerHttpService;

        }

        // GET: Developer
        public async Task<IActionResult> Index(MobileAppIndexViewModel mobileAppIndexRequest)
        {
            var mobileAppIndexViewModel = new MobileAppIndexViewModel
            {
                Search = mobileAppIndexRequest.Search,
                OrderAscendant = mobileAppIndexRequest.OrderAscendant,
                MobileApps = await _mobileAppHttpService.GetAllAsync(
                    mobileAppIndexRequest.OrderAscendant,
                    mobileAppIndexRequest.Search)
            };
           
            return View(mobileAppIndexViewModel);
        }

        // GET: Developer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAppViewModel = await _mobileAppHttpService.GetByIdAsync(id.Value);

            if (mobileAppViewModel == null)
            {
                return NotFound();
            }

            return View(mobileAppViewModel);
        }

        // GET: Developer/Create
        public async Task<IActionResult> Create()
        {
            await FillWithSelectedDevelopers();

            return View();
        }

        // POST: Developer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MobileAppViewModel mobileAppViewModel)
        {
            if (!ModelState.IsValid)
            {
                await FillWithSelectedDevelopers(mobileAppViewModel.DeveloperId);

                return View(mobileAppViewModel);
            }

            var mobileAppCreated = await _mobileAppHttpService.CreateAsync(mobileAppViewModel);

            return RedirectToAction(nameof(Details), new { id = mobileAppCreated.Id });
        }

        private async Task FillWithSelectedDevelopers(Guid? developerId = null)
        {
            var developers = await _developerHttpService.GetAllAsync(true);

            ViewBag.Developers = new SelectList(
                developers,
                nameof(DeveloperViewModel.Id),
                nameof(DeveloperViewModel.FirstName),
                developerId);
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAppViewModel = await _mobileAppHttpService.GetByIdAsync(id.Value);

            if (mobileAppViewModel == null)
            {
                return NotFound();
            }

            await FillWithSelectedDevelopers(mobileAppViewModel.DeveloperId);

            return View(mobileAppViewModel);
        }

        // POST: Developer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MobileAppViewModel mobileAppViewModel)
        {
            if (id != mobileAppViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await FillWithSelectedDevelopers(mobileAppViewModel.DeveloperId);

                return View(mobileAppViewModel);
            }
            try
            {
                await _mobileAppHttpService.EditAsync(mobileAppViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await MobileAppViewModelExistsAsync(mobileAppViewModel.Id);

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

            var mobileAppViewModel = await _mobileAppHttpService.GetByIdAsync(id.Value);

            if (mobileAppViewModel == null)
            {
                return NotFound();
            }
            return View(mobileAppViewModel);
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _mobileAppHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
 
        private async Task<bool> MobileAppViewModelExistsAsync(Guid id)
        {
            var developer = await _mobileAppHttpService.GetByIdAsync(id);

            var any = developer != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsUnusedName(string appName, Guid id)
        {
            return await _mobileAppHttpService.IsUnusedNameAsync(appName, id) 
                ? Json(true) 
                : Json($"O nome {appName} já está sendo usado.");
        }
    }
}
