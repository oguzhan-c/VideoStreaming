using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public CommunicationsController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _communicationService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetByIed(int id)
        {
            var result = _communicationService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _communicationService.Delete(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("add")]
        public IActionResult Add(Communication communication)
        {
            var result = _communicationService.Add(communication);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("Update")]
        IActionResult Update(Communication communication)
        {
            var result = _communicationService.Update(communication);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /*
            [HttpGet("getall")]
            public IActionResult GetAll();
            public IActionResult GetByIed(int id)
            [HttpDelete("delete")]
            public IActionResult Delete(int id)
            [HttpPut("add")]    
            public IActionResult Add()
            [HttpPut("Update")]
            public IActionResult Update()
         */
    }
}
