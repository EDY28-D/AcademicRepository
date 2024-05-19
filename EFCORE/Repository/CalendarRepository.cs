using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyect.EFCore.Repository
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly SeguimientoCurricularContext _bd;

        public CalendarRepository(SeguimientoCurricularContext bd)
        {
            _bd = bd;
        }

        public void agregar(CalendarEvent oCalendarEvent)
        {
            _bd.CalendarEvents.Add(oCalendarEvent);
            _bd.SaveChanges();
        }

        public void editar(CalendarEvent oCalendarEvent)
        {
            var obj = _bd.CalendarEvents.FirstOrDefault(p => p.EventId == oCalendarEvent.EventId);
            if (obj != null)
            {
                obj.AsesorId = oCalendarEvent.AsesorId;
                obj.EventStart = oCalendarEvent.EventStart;
                obj.EventEnd = oCalendarEvent.EventEnd;
                obj.EventDescription = oCalendarEvent.EventDescription;
                obj.StudentEmail = oCalendarEvent.StudentEmail;
                obj.IsAvailable = oCalendarEvent.IsAvailable;
                obj.Bhabilitado = oCalendarEvent.Bhabilitado;
                _bd.SaveChanges();
            }
        }

        public void eliminar(int idCalendar)
        {
            var obj = _bd.CalendarEvents.FirstOrDefault(p => p.EventId == idCalendar);
            if (obj != null)
            {
                obj.Bhabilitado = 0; // Soft delete, marcando como no habilitado
                _bd.SaveChanges();
            }
        }

        public List<CalendarEvent> filtrar(string id)
        {
            return _bd.CalendarEvents
                .Where(p => p.EventDescription.Contains(id) && p.Bhabilitado == 1)
                .ToList();
        }

        public List<CalendarEvent> listar()
        {
            return _bd.CalendarEvents
                .Where(p => p.Bhabilitado == 1)
                .ToList();
        }
    }
}
