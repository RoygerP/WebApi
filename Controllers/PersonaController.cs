using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/persona")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaServices _Services;

        public PersonaController(PersonaServices services)
        {
            _Services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaServices>>> HttpGet()
        {
            var Request = await _Services.Getall();
            return Ok(Request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaServices>> HttpGetByid(int id)
        {
            var Request = await _Services.GetById(id);
            return Ok(Request);
        }

        [HttpPost]
        public async Task<ActionResult> HttpPost([Bind("Nombre,Edad,Sexo")] PersonaModel persona)
        {
            var Request = await _Services.Crear(persona);
            return Ok(Request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> HttpPut(int id, PersonaModel persona)
        {
            var request = await _Services.UpdatePerson(id, persona);
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HttpDelete(int id)
        {
            var request = await _Services.Delete(id);
            return Ok(request);
        }
    }
}
