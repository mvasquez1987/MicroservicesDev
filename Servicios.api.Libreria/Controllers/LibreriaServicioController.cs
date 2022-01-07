using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Libreria.Core.Entities;
using Servicios.api.Libreria.Repository;

namespace Servicios.api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaServicioController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;

        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;

        private readonly IMongoRepository<EmpleadoEntity> _empleadoGenericoRepository;
        
        public LibreriaServicioController(IAutorRepository autorRepository, IMongoRepository<AutorEntity> autorGenericoRepository, IMongoRepository<EmpleadoEntity> empleadoGenericoRepository) {
            _autorRepository = autorRepository;
            _autorGenericoRepository = autorGenericoRepository;
            _empleadoGenericoRepository = empleadoGenericoRepository;

        }

        [HttpGet("empleadoGenerico")]
        public async Task<ActionResult<IEnumerable<EmpleadoEntity>>> GetEmpleadoGenerico()
        {

            var empleados = await _empleadoGenericoRepository.GetAll();

            return Ok(empleados);
        }

        [HttpGet("autorGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutorGenerico()
        {

            var autores = await _autorGenericoRepository.GetAll();

            return Ok(autores);
        }

        [HttpGet("autorGenerico/{id}")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutorId(string id)
        {

            var autores = await _autorGenericoRepository.GetById(id);

            return Ok(autores);
        }


        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores() {

            var autores = await _autorRepository.GetAutores();

            return Ok(autores);
        }

        [HttpPost("addAutor")]
        public async Task AddAutor(AutorEntity autor)
        {
            await _autorGenericoRepository.InsertDocument(autor);
        }

        [HttpPut("updateAutor")]
        public async Task UpdateAutor(AutorEntity autor)
        {
            await _autorGenericoRepository.UpdateDocument(autor);
        }

        [HttpDelete("deleteAutor/{id}")]
        public async Task DeleteAutor(string Id)
        {
            await _autorGenericoRepository.DeleteById(Id);
        }

    }
}
