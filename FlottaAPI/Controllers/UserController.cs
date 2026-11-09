using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FlottaContext _context;

        public UserController(FlottaContext context)
        {
            _context = context;
        }



        [HttpGet("GetAllUser")]

        public IActionResult GetAllUser()
        {


            List<User> user = _context.Users.ToList();
            try
            {
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Hiba az adatok lekérésekor.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren! " + ex.Message);
            }
        }

        [HttpGet("GetByIDUser/{id}")]

        public IActionResult GetByIdUser(int id)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(s => s.Id == id);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("Nincs ilyen user!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren! " + ex.Message);
            }
        }

        [HttpPost("PostUser")]


        public IActionResult PostUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás!");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba van a szerveren!" + ex.Message);
            }
        }


        [HttpPut("Change")]

        public IActionResult Put(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Missing datas");

                }
                User us = _context.Users.FirstOrDefault(s => s.Id == user.Id);
                if (us == null)
                {
                    return NotFound("Missing User");
                }

                _context.ChangeTracker.Clear();
                _context.Users.Update(us);
                _context.SaveChanges();
                return Ok("The User modified!");
            }
            catch (Exception ex)
            {

                return BadRequest("Error:" + ex.Message);
            }
        }
        [HttpDelete("Deleteuser")]


        public IActionResult DeleteUser(int id)
        {
            try
            {
                User user1 = _context.Users.Find(id);

                if (user1 == null)
                {
                    return NotFound("Nem található ilyen User!");
                }

                _context.Users.Remove(user1);
                _context.SaveChanges();

                return Ok("Sikeres törlés!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren!" + ex.Message);
            }
        }

    }
}
