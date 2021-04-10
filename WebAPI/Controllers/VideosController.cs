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

        [HttpPut("add")]
        public IActionResult Add([FromForm(Name = "Video")] Video video)
        {
            var result = _videoService.Add(video);

            if (result.Succcess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromForm(Name = "Video")] Video video)
        {
            var result = _videoService.Update(video);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("Delete")]
        public IActionResult Delete([FromForm(Name = "Id")] int id)
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
