using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.DTOs;
using PeopleAPI.Models;
using System;

namespace PeopleAPI.Controllers {
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeopleController(
            ApplicationDbContext context,
            IMapper mapper
         ) {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Servicio que da el Listado de Personas
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<ActionResult<List<PeopleDTO>>> GetPeopleAll() {
            try {
                var peoples = await context.Peoples
                                       .ToListAsync();

                return mapper.Map<List<PeopleDTO>>(peoples);
            } catch (Exception) {
                return StatusCode(500, "Ocurrió un error interno.");
            }            
        }

        /// <summary>
        /// Servicio que crea una persona en la base de datos.
        /// </summary>
        /// <param name="personCreateDTO"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreatePeople([FromBody] PeopleCreateDTO personCreateDTO) {
            try {
                var existsEmail = await context.Peoples.AnyAsync(x => x.Email == personCreateDTO.Email);

                if (existsEmail) {
                    return BadRequest($"El email: {personCreateDTO.Email} ya está registrado.");
                }

                var person = mapper.Map<People>(personCreateDTO);
                person.DateCreated = DateTime.Now;

                context.Add(person);
                await context.SaveChangesAsync();

                var personDTO = mapper.Map<PeopleDTO>(person);
                return Ok(new { message = "Persona creada con éxito", person = personDTO });
            } catch (Exception) {
                return StatusCode(500, "Ocurrió un error interno.");
            }            
        }

        /// <summary>
        /// Servicio que elimina una persona de la base de datos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeletePeople(int id) {
            try {
                var exist = await context.Peoples.AnyAsync(x => x.Id == id);

                if (!exist)
                    return NotFound($"No existe la persona con el id: {id}");
                
                context.Remove(new People() { Id = id });
                await context.SaveChangesAsync();

                return Ok(new { message = "Persona eliminada con éxito" });
            } catch (Exception) {
                return StatusCode(500, "Ocurrió un error interno.");
            }
            
        }

        /// <summary>
        /// Servicio que busca personas que coincidan por su nombre, apellido, edad o email.
        /// </summary>
        /// <param name="searchTerm">El término de búsqueda (nombre, apellido, edad o email).</param>
        /// <returns>La lista de personas encontradas.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<List<PeopleDTO>>> SearchPeople(string searchTerm) {
            try {
                var peopleQuery = context.Peoples.AsQueryable();

                if (int.TryParse(searchTerm, out int age)) {
                    peopleQuery = peopleQuery.Where(x => x.Age == age);
                } else{
                    peopleQuery = peopleQuery.Where(x => x.Name.Contains(searchTerm) || x.LastName.Contains(searchTerm) || x.Email.Contains(searchTerm));
                }

                var people = await peopleQuery.ToListAsync();

                if (people.Count == 0)
                    return NotFound($"No se encontraron personas que coincidan con el término de búsqueda: {searchTerm}");
                

                var peopleDTOs = mapper.Map<List<PeopleDTO>>(people);

                return Ok(peopleDTOs);
            } catch (Exception) {
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }
    }
}
