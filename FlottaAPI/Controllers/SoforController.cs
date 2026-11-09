using FlottaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoforController : ControllerBase
    {
        private FlottaContext _context;

        public SoforController(FlottaContext context)
        {
            _context = context;
        }
        


        [HttpGet("GetAll")]

        public IActionResult GetAllSofor()
        {
            

            List<Sofor> sofor = _context.Sofors.ToList();
            try 
            { 
                if(sofor != null)
                {
                    return Ok(sofor);
                }
                else
                {
                    return BadRequest("Hiba az adatok lekérésekor.");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren! " + ex.Message);
            }
        }


        [HttpGet("GetByIDSofor/{id}")]

        public IActionResult GetByIdSofor(int id)
        {
            try
            {
                Sofor sofor = _context.Sofors.FirstOrDefault(s => s.Id == id);

                if (sofor != null)
                {
                    return Ok(sofor);
                }
                else
                {
                    return NotFound("Nincs ilyen sofor!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren! " + ex.Message);
            }
        }

        [HttpPost("PostSofor")]


        public IActionResult PostSofor(Sofor sofor)
        {
            try
            {
                _context.Sofors.Add(sofor);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás!");
            }
            catch (Exception ex)
            {

                return StatusCode(500,"Hiba van a szerveren!" + ex.Message) ;
            }
        }

        [HttpPut("Change")]

        public IActionResult Put(Sofor sofor)
        {
            try
            {
                if(sofor == null)
                {
                    return BadRequest("Missing datas");

                }
                Sofor so = _context.Sofors.FirstOrDefault(s => s.Id == sofor.Id);
                if (so == null)
                {
                    return NotFound("Missing Driver");
                }

                _context.ChangeTracker.Clear();
                _context.Sofors.Update(so);
                _context.SaveChanges();
                return Ok("The driverdata modified!");
            }
            catch (Exception ex)
            {

                return BadRequest("Error:"+ex.Message);
            }
        }



        [HttpDelete("DeleteSofor")]


        public IActionResult DeleteSofor(int id)
        {
            try
            {
                Sofor sofor1 = _context.Sofors.Find(id);

                if (sofor1 == null)
                {
                    return NotFound("Nem található ilyen sofőr!");
                }

                _context.Sofors.Remove(sofor1);
                _context.SaveChanges();

                return Ok("Sikeres törlés!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren!" + ex.Message);
            }
        }

        [HttpGet("SoforJarmu/{id}")]

        public IActionResult GetSoforJarmu(int id)
        {
            try
            {
                Sofor vane = _context.Sofors.FirstOrDefault(s => s.Id == id);
                if (vane == null)
                {
                    return NotFound("Hiányzik a sofőr!");

                }
                var result = _context.Kikuldottjarmus.Where(j => j.SoforNavigation.Id == id).Select(j => j.Gepjarmu).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest("Hib!" + ex.Message);
            }
        }
    }
}
