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
    public class ProfilePicturesController : ControllerBase
    {
        private readonly IProfilePictureService _profilePictureService;

        public ProfilePicturesController(IProfilePictureService profilePictureService)
        {
            _profilePictureService = profilePictureService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _profilePictureService.GetAll();
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int id)
        {
            var result = _profilePictureService.GetByUserId(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromForm(Name = "Id")] int id)
        {
            var result = _profilePictureService.GetById(id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("ProfilePhoto"))] IFormFile file, [FromForm(Name = ("ProfilePicture"))] ProfilePicture profilePicture)
        {
            var result = _profilePictureService.Add(file, profilePicture);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("ProfilePicture"))] ProfilePicture profilePicture)
        {
            var result = _profilePictureService.Delete(profilePicture.Id);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("ProfilPhoto"))] IFormFile file, [FromForm(Name = ("profilPicture"))] ProfilePicture profilePicture)
        {
            var result = _profilePictureService.Update(file, profilePicture);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
