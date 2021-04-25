using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DislikesController : ControllerBase
    {
        private readonly IDislikeService _dislikeService;

        public DislikesController(IDislikeService dislikeService)
        {
            _dislikeService = dislikeService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _dislikeService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getbyid")]
        public IActionResult GetById([FromForm] int id)
        {
            var result = _dislikeService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("dislike"))] Dislike dislike)
        {
            var result = _dislikeService.Add(dislike);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm] int id)
        {
            var result = _dislikeService.Delete(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("dislike"))] Dislike dislike)
        {
            var result = _dislikeService.Update(dislike);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
