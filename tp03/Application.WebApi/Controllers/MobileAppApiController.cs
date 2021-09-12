using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
