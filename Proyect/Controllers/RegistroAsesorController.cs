using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Proyect.DTO.RegistroAsesor;
using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;

namespace Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAsesorController : Controller
    {
        private readonly IRegistroRepository _registroasesor;
        private readonly IUnitOfWorkRepository _unit;

        public RegistroAsesorController(IRegistroRepository registroasesor, IUnitOfWorkRepository unit)
        {
            _registroasesor = registroasesor;
            _unit = unit;
        }

        [HttpGet("listar")]
        public ActionResult<List<ListarRegistroAsesorDTO>> Listar()
        {
            var registros = _registroasesor.listar().Select(p => new ListarRegistroAsesorDTO
            {
                IdAsesor = p.AsesorId,
                nombre = p.FullName,
                Especialidad = p.Speciality,
                Habilitado = p.Bhabilitado,
            }).ToList();
            return Ok(registros);
        }

        [HttpGet("filtrar/{nombre}")]
        public ActionResult<List<ListarRegistroAsesorDTO>> Filtrar(string nombre)
        {
            var registros = _registroasesor.filtrar(nombre).Select(p => new ListarRegistroAsesorDTO
            {
                IdAsesor = p.AsesorId,
                nombre = p.FullName,
                Especialidad = p.Speciality,
                Habilitado = p.Bhabilitado,
            }).ToList();
            return Ok(registros);
        }

        [HttpDelete("eliminar/{idRegistroAsesor}")]
        public ActionResult EliminarRegistroAsesor([FromRoute] int idRegistroAsesor)
        {
            try
            {
                _registroasesor.eliminar(idRegistroAsesor);
                _unit.SaveChanges();
                return Ok("Registro eliminado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar: " + ex.Message);
            }
        }

        [HttpPost("guardar")]
        public ActionResult GuardarRegistro([FromBody] RegistroAsesor oRegistroAsesor)
        {
            try
            {
                if (oRegistroAsesor.AsesorId == 0)
                {
                    _registroasesor.agregar(oRegistroAsesor);
                }
                else
                {
                    _registroasesor.editar(oRegistroAsesor);
                }
                _unit.SaveChanges();
                return Ok("Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al guardar: " + ex.Message);
            }
        }
        [HttpPut("editar/{id}")]  // Uso de HttpPut como es típico para operaciones de edición
        public ActionResult EditarRegistro([FromRoute] int id, [FromBody] RegistroAsesor oRegistroAsesor)
        {
            try
            {
                var registroExistente = _registroasesor.listar().FirstOrDefault(r => r.AsesorId == id);
                if (registroExistente == null)
                {
                    return NotFound("Registro no encontrado.");
                }

                // Actualizar propiedades del registro existente
                registroExistente.FullName = oRegistroAsesor.FullName;
                registroExistente.Email = oRegistroAsesor.Email;
                registroExistente.PhoneNumber = oRegistroAsesor.PhoneNumber;
                registroExistente.Speciality = oRegistroAsesor.Speciality;
                registroExistente.Role = oRegistroAsesor.Role;
                registroExistente.Bhabilitado = oRegistroAsesor.Bhabilitado;

                _registroasesor.editar(registroExistente);
                _unit.SaveChanges();

                return Ok("Registro actualizado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al editar: " + ex.Message);
            }
        }

    }
}
