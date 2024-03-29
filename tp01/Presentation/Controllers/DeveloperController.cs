﻿using System;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(
            IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        // GET: Developer
        public async Task<IActionResult> Index(DeveloperIndexViewModel developerIndexRequest)
        {
            var developerIndexViewModel = new DeveloperIndexViewModel
            {
                Search = developerIndexRequest.Search,
                OrderAscendant = developerIndexRequest.OrderAscendant,
                Developers = await _developerService.GetAllAsync(
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

            var developerModel = await _developerService.GetByIdAsync(id.Value);

            if (developerModel == null)
            {
                return NotFound();
            }

            var developerViewModel = DeveloperViewModel.From(developerModel);

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

            var developerModel = developerViewModel.ToModel();
            var developerCreated = await _developerService.CreateAsync(developerModel);

            return RedirectToAction(nameof(Details), new {id = developerCreated.Id});
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developerModel = await _developerService.GetByIdAsync(id.Value);

            if (developerModel == null)
            {
                return NotFound();
            }

            var developerViewModel = DeveloperViewModel.From(developerModel);

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

            var developerModel = developerViewModel.ToModel();
            try
            {
                await _developerService.EditAsync(developerModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await DeveloperModelExistsAsync(developerModel.Id);

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

            var developerModel = await _developerService.GetByIdAsync(id.Value);

            if (developerModel == null)
            {
                return NotFound();
            }

            var developerViewModel = DeveloperViewModel.From(developerModel);

            return View(developerViewModel);
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _developerService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DeveloperModelExistsAsync(Guid id)
        {
            var developer = await _developerService.GetByIdAsync(id);

            var any = developer != null;

            return any;
        }
    }
}
