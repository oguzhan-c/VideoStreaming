using Business.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailService _userDetailService;

        public UserDetailsController(IUserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userDetailService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userDetailService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete")]
        //UI tarafında tekrardan bakılacak.
        public IActionResult Delete(UserDetail userDetail)
        {
            var result = _userDetailService.Delete(userDetail.Id);
            if (result.Succcess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("add")]
        public IActionResult Add(UserDetail userDetail)
        {
            var result = _userDetailService.Add(userDetail);
            if (result.Succcess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("addProfilePhoto")]
        public IActionResult AddProfilePhoto([FromForm(Name = "profilePhoto")] IFormFile profilePhotoFile, [FromForm] int id)
        {
            var result = _userDetailService.AddProfilePhoto(profilePhotoFile, id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPut("update")]
        public IActionResult Update(UserDetail userDetail)
        {
            var result = _userDetailService.Update(userDetail);
            if (result.Succcess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
