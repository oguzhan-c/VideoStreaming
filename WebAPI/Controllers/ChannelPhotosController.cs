using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelPhotosController : ControllerBase
    {
        private readonly IChannelPhotoService _channelPhotoService;

        public ChannelPhotosController(IChannelPhotoService channelPhotoService)
        {
            _channelPhotoService = channelPhotoService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _channelPhotoService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById([FromForm(Name = "Id")] int id)
        {
            var result = _channelPhotoService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("file"))] IFormFile file, [FromForm(Name = ("ChannelPhoto"))] ChannelPhoto channelPhoto)
        {
            var result = _channelPhotoService.Add(file, channelPhoto);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int id)
        {
            var result = _channelPhotoService.Delete(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("file"))] IFormFile file, [FromForm(Name = ("ChannelPhoto"))] ChannelPhoto channelPhoto)
        {
            var result = _channelPhotoService.Update(file, channelPhoto);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
