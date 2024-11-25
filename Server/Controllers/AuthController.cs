using EquipmentsSystem.Server.Services.AuthService;
using EquipmentsSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Data;
using System.Security.Claims;

namespace EquipmentsSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;

        public AuthController(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(
                new User
                {
                    Email = request.Email,
                    Role = request.Role,
                    Rank = request.Rank,
                    DisplayName = request.DisplayName
                },
                request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            //try
            //{
            //    updateIP(request);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            return Ok(response);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<ServiceResponse<List<UserView>>>> GetUsers(string? email ,int currentPageNumber = 1, int pageSize = 10)
        {
            var result = await _authService.GetUsers(
                currentPageNumber: currentPageNumber,
                pageSize: pageSize,
                email:email
                );
            return Ok(result);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] UserChangePassword user)
        {
            var response = await _authService.ChangePassword(user.Id, user.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("UpdateUser"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(UserView obj)
        {
            var response = await _authService.UpdateUser(obj);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser([FromQuery] int obj)
        {
            var response = await _authService.DeleteUser(obj);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        //public void updateIP(UserLogin request)
        //{
        //    //var user = await _context.Users
        //    //    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()));

        //    string ip = GetIpCode(HttpContext);
        //    IPUser userIP = new IPUser { ip = ip, time = DateTime.Now, unit = request.Email.ToLower()};
        //    _ipuserService.AddIPUser(userIP);
        //}

        //public string GetIpCode(Microsoft.AspNetCore.Http.HttpContext httpcontext)
        //{
        //    string ip = httpcontext.GetServerVariable("HTTP_X_FORWARDED_FOR");
        //    //string ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = httpcontext.GetServerVariable("REMOTE_ADDR");
        //        //ip = context.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = httpcontext.Connection.RemoteIpAddress.ToString();
        //    }

        //    return ip;
        //    // Other code
        //}
    }
}
