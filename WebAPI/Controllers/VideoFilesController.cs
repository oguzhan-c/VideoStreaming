using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoFilesController : ControllerBase
    {
        private readonly IVideoFileService _videoFileService;

        public VideoFilesController(IVideoFileService videoFileService)
        {
            _videoFileService = videoFileService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _videoFileService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getById")]
        public IActionResult GetById([FromForm(Name = "Id")] int id)
        {
            var result = _videoFileService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("file"))] IFormFile file, [FromForm(Name = ("VideoFile"))] VideoFile videoFile)
        {
            var result = _videoFileService.Add(file, videoFile);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int id)
        {
            var result = _videoFileService.Delete(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("file"))] IFormFile file, [FromForm(Name = ("VideoFile"))] VideoFile videoFile)
        {
            var result = _videoFileService.Update(file, videoFile);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
