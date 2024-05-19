using Microsoft.AspNetCore.Mvc;
using Proyect.DTO.Calendar;
using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using System;
using System.Linq;

namespace Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : Controller
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IUnitOfWorkRepository _unit;

        public CalendarController(ICalendarRepository calendarRepository, IUnitOfWorkRepository unit)
        {
            _calendarRepository = calendarRepository;
            _unit = unit;
        }

        [HttpGet("listar")]
        public ActionResult<List<ListarCalendar>> Listar()
        {
            var eventos = _calendarRepository.listar().Select(e => new ListarCalendar
            {
                EventId = e.EventId,
                AsesorId = e.AsesorId,
                EventStart = e.EventStart,
                EventEnd = e.EventEnd,
                EventDescription = e.EventDescription
            }).ToList();
            return Ok(eventos);
        }

        [HttpGet("filtrar/{description}")]
        public ActionResult<List<ListarCalendar>> Filtrar(string description)
        {
            var eventos = _calendarRepository.filtrar(description).Select(e => new ListarCalendar
            {
                EventId = e.EventId,
                AsesorId = e.AsesorId,
                EventStart = e.EventStart,
                EventEnd = e.EventEnd,
                EventDescription = e.EventDescription
            }).ToList();
            return Ok(eventos);
        }
        //si se trabajo eh s
        [HttpPost("guardar")]
        public ActionResult Guardar([FromBody] CalendarEvent calendarEvent)
        {
            try
            {
                if (calendarEvent.EventId == 0)
                {
                    _calendarRepository.agregar(calendarEvent);
                }
                else
                {
                    _calendarRepository.editar(calendarEvent);
                }
                _unit.SaveChanges();
                return Ok("Evento guardado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al guardar el evento: " + ex.Message);
            }
        }

        [HttpDelete("eliminar/{eventId}")]
        public ActionResult Eliminar(int eventId)
        {
            try
            {
                _calendarRepository.eliminar(eventId);
                _unit.SaveChanges();
                return Ok("Evento eliminado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el evento: " + ex.Message);
            }
        }

        [HttpPut("editar/{eventId}")]
        public ActionResult Editar(int eventId, [FromBody] CalendarEvent calendarEvent)
        {
            try
            {
                var existingEvent = _calendarRepository.listar().FirstOrDefault(e => e.EventId == eventId);
                if (existingEvent == null)
                {
                    return NotFound("Evento no encontrado.");
                }

                existingEvent.AsesorId = calendarEvent.AsesorId;
                existingEvent.EventStart = calendarEvent.EventStart;
                existingEvent.EventEnd = calendarEvent.EventEnd;
                existingEvent.EventDescription = calendarEvent.EventDescription;
                existingEvent.StudentEmail = calendarEvent.StudentEmail;
                existingEvent.IsAvailable = calendarEvent.IsAvailable;
                existingEvent.Bhabilitado = calendarEvent.Bhabilitado;

                _calendarRepository.editar(existingEvent);
                _unit.SaveChanges();

                return Ok("Evento actualizado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al editar el evento: " + ex.Message);
            }
        }
    }
}
