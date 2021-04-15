using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverImagesController : ControllerBase
    {
        private readonly ICoverImageService _coverImageService;

        public CoverImagesController(ICoverImageService coverImageService)
        {
            _coverImageService = coverImageService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _coverImageService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getById")]
        public IActionResult GetById([FromForm(Name = "Id")] int id)
        {
            var result = _coverImageService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("file"))] IFormFile file, [FromForm(Name = ("CoverImage"))] CoverImage coverImage)
        {
            var result = _coverImageService.Add(file, coverImage);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int id)
        {
            var result = _coverImageService.Delete(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("file"))] IFormFile file, [FromForm(Name = ("CoverImage"))] CoverImage coverImage)
        {
            var result = _coverImageService.Update(file, coverImage);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
