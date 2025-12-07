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
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public List<StudentGetAllResponse> GetAll()
        {
            return StudentStorage.Students.Select(x=> new StudentGetAllResponse
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
            var s = StudentStorage.Students.Where(x => x.Id == id).Select(x => new StudentGetbyIdResponse
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
            var maxID = StudentStorage.Students.Max(x => x.Id);
            var s = new Student
            {
                Id = maxID +1,
                Ime = request.Ime,
                Prezime = request.Prezime,
                OpstinaRodjenjaId = request.OpstinaRodjenjaId,
                DatumRodjenja = request.DatumRodjenja,
            };
            StudentStorage.Students.Add(s);
            return s.Id;
        }

        [HttpDelete]
        public IActionResult Obrisi(int studentId)
        {
            var s = StudentStorage.Students.FirstOrDefault(x => x.Id == studentId);
            if(s == null)
            {
                return BadRequest();
            }
            StudentStorage.Students.Remove(s);
            return Ok();
        }
    }
}
