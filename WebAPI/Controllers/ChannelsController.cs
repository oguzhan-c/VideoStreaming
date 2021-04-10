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
    public class ChannelsController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelsController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet("getall")]
        public IActionResult GeAll()
        {
            var result = _channelService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _channelService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("add")]
        public IActionResult Add(Channel channel)
        {
            var result = _channelService.Add(channel);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("Update")]
        public IActionResult Update(Channel channel)
        {
            var result = _channelService.Update(channel);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("Delete")]
        public IActionResult Delete(int id)
        {
            var result = _channelService.Delete(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
