using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideosController(IVideoService videoService)
        {
            _videoService = videoService;
        }


        [HttpGet("getAll")]
        public IActionResult GeAll()
        {
            var result = _videoService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _videoService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getThumbnailById")]
        public IActionResult GetThumbnail(int id)
        {
            var result = _videoService.GetThumbnail(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getVideoFileById")]
        public IActionResult GetVideoFile(int id)
        {
            var result = _videoService.GetVideoFile(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPut("add")]
        public IActionResult Add(Video video)
        {
            var result = _videoService.Add(video);

            if (result.Succcess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPut("addVideoThumbnail")]
        public IActionResult AddVideoThumbnail([FromForm(Name = "thumbnailFile")] IFormFile thumbnailFile , [FromForm] int id)
        {
            var result = _videoService.AddVideoFile(thumbnailFile, id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("addVideoFile")]
        public IActionResult AddVideoFile([FromForm(Name = "videoFile")] IFormFile videoFile, [FromForm] int id)
        {
            var result = _videoService.AddVideoFile(videoFile, id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = "videoFile")] IFormFile videoFile , [FromForm(Name = "thumbnailFile")] IFormFile thumbnailFile , [FromForm] Video video)
        {
            var result = _videoService.Update(videoFile, thumbnailFile, video);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete([FromForm] int id)
        {
            var result = _videoService.Delete(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
