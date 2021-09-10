using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class MobileAppController : Controller
    {
        private readonly IMobileAppService _mobileAppService;
        private readonly IDeveloperService _developerService;

        public MobileAppController(
            IMobileAppService mobileAppService,
            IDeveloperService developerService)
        {
            _mobileAppService = mobileAppService;
            _developerService = developerService;

        }

        // GET: Developer
        public async Task<IActionResult> Index(MobileAppIndexViewModel mobileAppIndexRequest)
        {
            var mobileAppIndexViewModel = new MobileAppIndexViewModel
            {
                Search = mobileAppIndexRequest.Search,
                OrderAscendant = mobileAppIndexRequest.OrderAscendant,
                MobileApps = await _mobileAppService.GetAllAsync(
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

            var mobileAppModel = await _mobileAppService.GetByIdAsync(id.Value);

            if (mobileAppModel == null)
            {
                return NotFound();
            }

            var mobileAppViewModel = MobileAppViewModel.From(mobileAppModel);

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

            var mobileAppModel = mobileAppViewModel.ToModel();

            var mobileAppCreated = await _mobileAppService.CreateAsync(mobileAppModel);

            return RedirectToAction(nameof(Details), new { id = mobileAppCreated.Id });
        }

        private async Task FillWithSelectedDevelopers(Guid? developerId = null)
        {
            var developers = await _developerService.GetAllAsync(true);

            ViewBag.Developers = new SelectList(
                developers,
                nameof(DeveloperModel.Id),
                nameof(DeveloperModel.FirstName),
                developerId);
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAppModel = await _mobileAppService.GetByIdAsync(id.Value);

            if (mobileAppModel == null)
            {
                return NotFound();
            }

            await FillWithSelectedDevelopers(mobileAppModel.DeveloperId);

            var mobileAppViewModel = MobileAppViewModel.From(mobileAppModel);

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

            var mobileAppModel = mobileAppViewModel.ToModel();
            try
            {
                await _mobileAppService.EditAsync(mobileAppModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await MobileAppModelExistsAsync(mobileAppModel.Id);

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

            var mobileAppModel = await _mobileAppService.GetByIdAsync(id.Value);

            if (mobileAppModel == null)
            {
                return NotFound();
            }

            var mobileAppViewModel = MobileAppViewModel.From(mobileAppModel);
            return View(mobileAppViewModel);
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _mobileAppService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
 
        private async Task<bool> MobileAppModelExistsAsync(Guid id)
        {
            var developer = await _mobileAppService.GetByIdAsync(id);

            var any = developer != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsUnusedName(string appName, Guid id)
        {
            return await _mobileAppService.IsUnusedNameAsync(appName, id) 
                ? Json(true) 
                : Json($"O nome {appName} já está sendo usado.");
        }
    }
}
