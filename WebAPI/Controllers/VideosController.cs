using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

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

        [HttpGet("getall")]
        public IActionResult GeAll()
        {
            var result = _videoService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _videoService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getthumbnailbyid")]
        public IActionResult GetThumbnail(int id)
        {
            var result = _videoService.GetThumbnail(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getvideovilebyid")]
        public IActionResult GetVideoFile(int id)
        {
            var result = _videoService.GetVideoFile(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Video video)
        {
            var result = _videoService.Add(video);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addvideothumbnail")]
        public IActionResult AddVideoThumbnail([FromForm(Name = "thumbnailFile")] IFormFile thumbnailFile, [FromForm] int id)
        {
            var result = _videoService.AddVideoThumbnail(thumbnailFile, id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addvideofile")]
        public IActionResult AddVideoFile([FromForm(Name = "videoFile")] IFormFile videoFile, [FromForm] int id)
        {
            var result = _videoService.AddVideoFile(videoFile, id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        public ActionResult Update([FromForm(Name = "videofile")] IFormFile videoFile, [FromForm(Name = "thumbnailfile")] IFormFile thumbnailFile, Video video)
        {
            var result = _videoService.Update(videoFile, thumbnailFile, video);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
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
