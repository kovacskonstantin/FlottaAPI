using Argon2Sharp;
using FlottaAPI.DTOs;
using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private FlottaContext _context;

        public LoginController(FlottaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO logintDTO)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.LoginName == logintDTO.LoginName);
                if(user == null)
                {
                    return NotFound("Missing user.");
                }
                string phcString = user.Hash;
                var(isValid,parameters)=Argon2Sharp.Argon2PhcFormat.VerifyPhcString(logintDTO.Password, phcString );

                if (isValid)
                {
                    return Ok("token");

                }
                else
                {
                    return BadRequest("Wrong password!");
                }
            }

            catch (Exception ex)
            {

                return BadRequest("Something wrong happend!" + ex.Message);
            }
        }
    }
}
