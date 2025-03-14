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
    public class Persona_Controller : ControllerBase
    {
        private readonly Persona_Services _Services;

        public Persona_Controller(Persona_Services services)
        {
            _Services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona_Services>>> HttpGet()
        {
            var Request = await _Services.Getall();
            if (Request is null)
            {
                return NotFound("No hay personas actualmente");
            }
            return Ok(Request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona_Services>> HttpGetByid(int id)
        {
            var Request = await _Services.GetById(id);

            if (Request is null)
            {
                return NotFound("Persona no encontrada");
            }

            return Ok(Request);
        }

        [HttpPost]
        public async Task<ActionResult> HttpPost([Bind("Nombre,Sexo")] Persona_Model persona)
        {
            var Request = await _Services.Crear(persona);

            if (Request is null)
            {
                return NotFound("Persona no guardada exitosamente");
            }

            return Ok(Request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> HttpPut(int id, Persona_Model persona)
        {
            var request = await _Services.UpdatePerson(id, persona);
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HttpDelete(int id)
        {
            var PersonExist = await _Services.GetById(id);

            if (PersonExist is null)
            {
                return NotFound("Persona no encontrada");
            }
            var request = await _Services.Delete(id);
            return Ok(request);
        }
    }
}
