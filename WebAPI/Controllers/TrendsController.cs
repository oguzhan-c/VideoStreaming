using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendsController : ControllerBase
    {
        private readonly ITrendService _trendService;

        public TrendsController(ITrendService trendService)
        {
            _trendService = trendService;
        }

        [HttpGet("getAll")]
        public IActionResult GeAll()
        {
            var result = _trendService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _trendService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("add")]
        public IActionResult Add([FromForm(Name = "Trend")] Trend trend)
        {
            var result = _trendService.Add(trend);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = "Trend")] Trend trend)
        {
            var result = _trendService.Update(trend);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete([FromForm(Name = "Id")]int id)
        {
            var result = _trendService.Delete(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
