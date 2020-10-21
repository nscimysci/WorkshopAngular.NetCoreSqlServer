using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPAPI.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace APPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {

        ILogger<ProductController> _logger;
        public DatabaseContext Context { get; }
        public IConfiguration Config { get; }
        public ProductController(ILogger<ProductController> logger, DatabaseContext Context, IConfiguration Config)
        {
            this.Config = Config;
            this.Context = Context;
            this._logger = logger;
        }


        [HttpGet("print")]
        public IActionResult Get()
        {
            try
            {
                return Ok("API OK");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var data = await this.Context.Products.ToListAsync();

                return Ok(new { result = data, message = "success" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Log getAll: {ex.Message}");
                return StatusCode(500, new { result = "", message = ex.Message });
            }
        }



    }

}
