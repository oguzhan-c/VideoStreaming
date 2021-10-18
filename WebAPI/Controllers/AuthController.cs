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
        private readonly IUserService _userService;


        public AuthController(IAuthService authService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _userService = userService;
           
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
                //userForLoginDto.IsActive = true;
                return Ok(result);
            }

            return BadRequest(result);
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
