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
    public class MobileAppApiController : ControllerBase
    {
        private readonly IMobileAppService _mobileAppService;

        public MobileAppApiController(IMobileAppService mobileAppService)
        {
           _mobileAppService = mobileAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MobileAppModel>>> Get()
        {
            var mobileApps = await _mobileAppService.GetAllAsync(orderAscendant: true);

            return Ok(mobileApps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MobileAppModel>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var mobileAppModel = await _mobileAppService.GetByIdAsync(id);

            if (mobileAppModel == null)
            {
                return NotFound();
            }

            return Ok(mobileAppModel);
        }

        [HttpPost]
        public async Task<ActionResult<MobileAppModel>> Post([FromBody] MobileAppModel mobileAppModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(mobileAppModel);
            }
            
            var mobileAppCreated = await _mobileAppService.CreateAsync(mobileAppModel);

            return Ok(mobileAppCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MobileAppModel>> Put(Guid id, [FromBody] MobileAppModel mobileAppModel)
        {
            if (id != mobileAppModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(mobileAppModel);
            }
            
            try
            {
                var editedMobileApp = await _mobileAppService.EditAsync(mobileAppModel);

                return Ok(editedMobileApp);
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

            await _mobileAppService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("IsUnusedName/{appName}/{id:guid}")]
        public async Task<IActionResult> IsUnusedName(string appName, Guid id)
        {
            var isUsed = await _mobileAppService.IsUnusedNameAsync(appName, id);

            return Ok(isUsed);
        }
    }
}
