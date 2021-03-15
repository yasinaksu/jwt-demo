using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniApp3.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly static string[] Summaries = new[]
        {
            "Cool","Freezing","Chilly","Mild","Warm","Balmy"
        };
        [HttpGet]
        public IActionResult GetSummary()
        {
            var random = new Random();
            var index = random.Next(0, Summaries.Length);
            var userName = User.Identity.Name;
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return Ok($"Hava Durumu İşlemleri => Hava : {Summaries[index]}");
        }
    }
}
