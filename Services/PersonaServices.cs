using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Identity.Client;
using WebApi.Data;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Services
{
    public class PersonaServices
    {
        private readonly ContextDatabase _context;

        public Persona_Services(ContextDatabase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonaModel>> Getall()
        {
            var Personas = await _context.personas.ToListAsync();
            return Personas;
        }

        public async Task<PersonaModel?> GetById(int id)
        {
            var Personas = _context.personas.FindAsync(id);
            if (Personas is null)
            {
                throw new NotFoundException("No se encuntra la persona");
            }
            return Personas;
        }

        public async Task<PersonaModel> Crear(PersonaModel persona)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.AddAsync(persona);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return persona;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return CustomException("Persona no creadad exitosamente");
            }
        }

        public async Task<PersonaModel> UpdatePerson(int id, PersonaModel persona)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var PersonExist = await _context.personas.FindAsync(id);
                if (PersonExist is null)
                {
                    return CustomException("Persona no encontrada");
                }

                PersonExist.Nombre = persona.Nombre;
                PersonExist.Sexo = persona.Sexo;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return PersonExist;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return CustomException("Error de actualizacion");
            }
        }

        public async Task<PersonaModel> Delete(int id)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var persona = await _context.personas.FindAsync(id);

                if (persona is null)
                {
                    return CustomException("Persona no encontrada");
                }
                _context.personas.Remove(persona);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return persona;
            }
            catch
            {
                await transaction.RollbackAsync();
                return CustomException("Error al eliminar persona");
            }
        }

        private PersonaModel CustomException(string Message)
        {
            throw new NotFoundException(Message);
        }
    }
}
