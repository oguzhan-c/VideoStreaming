using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _channelService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _channelService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getChannelPhoto")]
        public IActionResult GetChannelPhoto(int id)
        {
            var result = _channelService.GetChannelPhoto(id);

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

        [HttpPost("addChannelPhoto")]
        public IActionResult AddChannelPhoto([FromForm(Name = "channelPhoto")] IFormFile channelPhotoFile,[FromForm]int id)
        {
            var result = _channelService.AddChannelPhoto(channelPhotoFile, id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = "channelPhoto")]IFormFile channelPhotoFile,[FromForm] Channel channel)
        {
            var result = _channelService.Update(channelPhotoFile,channel);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("delete")]
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
