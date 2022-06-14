using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reserveit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructStringController : BaseController
    {
        // GET: api/<ConstructStringController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = Request.Cookies["ConstructString"];

            return Ok(result);
        }

        // POST api/<ConstructStringController>
        [HttpPost]
        public IActionResult Post(string txtValue)
        {
            var headerValue = Request.Headers["page-size"];
            int pageSize = Convert.ToInt32(headerValue);
            if (headerValue.Any() == false && txtValue.Length > pageSize) return BadRequest();
            int pageNumber = (int)Math.Ceiling(txtValue.Length / (double)pageSize);
            string result = "";
            for (int i = 0; i < pageNumber - 1; i++)
            {
                result = result + txtValue.Substring(i * pageSize, pageSize);
                result = result + "\n";
            }
            result = result.Replace("\n", Environment.NewLine);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            HttpContext.Response.Cookies.Append("ConstructString", result, new CookieOptions { Expires = DateTime.Now.AddDays(30) });
            return Created("~api/ConstructString", result);
        }
    }
}
