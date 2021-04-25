using System;
using System.Linq;
using System.Security.Claims;
using System.Web.WebPages;
using Microsoft.AspNetCore.Mvc;
using Business.Abstruct;
using Business.BusinessAspects.Autofac;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.Extensions;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserService _userService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Succcess)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Succcess)
            {
                userForLoginDto.IsActive = true;
                return Ok(result);
            }

            return BadRequest(result);
        }


        [Authorize]//hangi kullanıcı log out yapacak onu anlamak için yazıldı
        [HttpDelete("logout")]
        public IActionResult LogOut() 
        {
            var rawUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("NameIdentifier");//log out yapılmak istenen user in id si getirildi.? ile null kontrolü yapıldı

            if (rawUserId == null)
            {
                return Unauthorized();
            }

            var result = _authService.LogOut(rawUserId.AsInt());
            
            if (result.Succcess)
            {
                return BadRequest(result);
            }

            return NoContent();
        }


        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _userService.CheckIfUserAlreadyExist(userForRegisterDto.Email);
            if (!userExists.Succcess)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Succcess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
