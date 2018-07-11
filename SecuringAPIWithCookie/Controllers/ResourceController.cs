using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAPIWithCookie.Controllers
{
    public class Entity
    {
        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    
    public class ResourceController : ControllerBase
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet("AdminGet")]
        public async Task<IActionResult> AdminGetAsync()
        {
            return await Task.FromResult(new ObjectResult(new Entity() { Name = "administrator role" }));
        }

        [Authorize(Roles = "otherRole")]
        [HttpGet("OtherGet")]
        public async Task<IActionResult> OtherGetAsync()
        {
            return await Task.FromResult(new ObjectResult(new Entity() { Name = "OtherRole" }));
        }
    }
}
