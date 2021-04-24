using ItSerwis.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSerwis.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserLoginRepository userLoginRepository;
        private IConfigurationSection Section { get; set; }
        private int Code500 { get; set; } = StatusCodes.Status500InternalServerError;
        private int Code200 { get; set; } = StatusCodes.Status200OK;
        private int Code400 { get; set; } = StatusCodes.Status400BadRequest;

        public UserLoginController(IUserLoginRepository userLoginRepository, IConfiguration _config)
        {
            this.userLoginRepository = userLoginRepository;
            configuration = _config;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                Console.WriteLine($"{nameof(GetUsers)}\nCode: [{Code200}]");
                
                return Ok(await userLoginRepository.GetUsers());
            }
            catch (Exception err)
            {
                Console.WriteLine($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, Section.GetValue<string>("RetreiveFromDbError"));
            }

        }
    }
}
