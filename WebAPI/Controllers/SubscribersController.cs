using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _subscriberService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getById")]
        public IActionResult GetById([FromForm(Name = "id")] int id)
        {
            var result = _subscriberService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("subscriber"))] Subscriber subscriber)
        {
            var result = _subscriberService.Add(subscriber);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("id"))] int id)
        {
            var result = _subscriberService.Delete(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("subscriber"))] Subscriber subscriber)
        {
            var result = _subscriberService.Update(subscriber);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
