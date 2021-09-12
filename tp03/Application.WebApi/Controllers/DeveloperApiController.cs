using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeveloperApiController : ControllerBase

    {
        private readonly IDeveloperService _developerService;

        public DeveloperApiController(IDeveloperService developerService)
        {
           _developerService = developerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperModel>>> Get()
        {
            var developers = await _developerService.GetAllAsync(orderAscendant: true);

            return Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperModel>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var developerModel = await _developerService.GetByIdAsync(id);

            if (developerModel == null)
            {
                return NotFound();
            }

            return Ok(developerModel);
        }

        [HttpPost]
        public async Task<ActionResult<DeveloperModel>> Post([FromBody] DeveloperModel developerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(developerModel);
            }
            
            var developerCreated = await _developerService.CreateAsync(developerModel);

            return Ok(developerCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeveloperModel>> Put(Guid id, [FromBody] DeveloperModel developerModel)
        {
            if (id != developerModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(developerModel);
            }
            
            try
            {
                var editedDeveloperMode = await _developerService.EditAsync(developerModel);

                return Ok(editedDeveloperMode);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _developerService.DeleteAsync(id);

            return Ok();
        }
    }
}
