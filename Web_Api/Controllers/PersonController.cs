using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api.Model;
using Web_Api.Services;

namespace Web_Api.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        //GET FIND ALL
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        //GET FIND BY ID
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            if(person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


        //POST CREATE
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if(person == null)
            {
                return BadRequest();
            }

            return Ok(_personService.Create(person));
        }


        //UPDATE
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            return Ok(_personService.Update(person));
        }


        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
