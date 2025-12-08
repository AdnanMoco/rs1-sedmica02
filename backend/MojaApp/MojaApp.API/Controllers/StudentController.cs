using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Controllers.Dtos;
using MojaApp.API.Data;
using MojaApp.API.Models;

namespace MojaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(MyDBContext db) : ControllerBase
    {
        [HttpGet]
        public List<StudentGetAllResponse> GetAll()
        {
            return db.Student.Select(x=> new StudentGetAllResponse
            (
                x.Id,
                x.Ime,
                x.Prezime,
                x.OpstinaRodjenja == null ? null : new StudentGetAllResponseOpstina(x.OpstinaRodjenja.Description, "123")
                )
            ).ToList();
        }

        [HttpGet("(id)")] // jer ima vec jedna ova Get putanja, pa se preliću, i ovu malo izmijeniš
        public StudentGetbyIdResponse GetById(int id)
        {
            var s = db.Student.Where(x => x.Id == id).Select(x => new StudentGetbyIdResponse
            (
                x.Id,
                x.Ime,
                x.Prezime,
                x.SlikaStudenta,
                x.OpstinaRodjenja == null ? null : new StudentGetbyIdResponseOpstina(x.OpstinaRodjenja.Description, "123")
                )
            )
                .FirstOrDefault();

            if (s == null)
                throw new Exception("Nema studenta");

            return s;
        }

        [HttpPost]
        public int Dodaj([FromBody] StudentDodajRequest request)
        {
            
            var s = new Student
            { 
                
                Ime = request.Ime,
                Prezime = request.Prezime,
                BrojIndeksa =" ",
                OpstinaRodjenjaId = request.OpstinaRodjenjaId,
                DatumRodjenja = request.DatumRodjenja,
                CreatedTime = DateTime.UtcNow,
                SlikaStudenta ="/nesto.jpg"
            };
            db.Student.Add(s);
            db.SaveChanges();
            return s.Id;
        }

        [HttpDelete]
        public IActionResult Obrisi(int studentId)
        {
            var s = db.Student.FirstOrDefault(x => x.Id == studentId);
            if(s == null)
            {
                return BadRequest();
            }
            db.Student.Remove(s);
            db.SaveChanges();
            return Ok();
        }
    }
}
