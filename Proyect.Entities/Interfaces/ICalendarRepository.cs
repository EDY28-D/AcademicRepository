using Proyect.Entities.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Entities.Interfaces
{
    public  interface ICalendarRepository
    {
        List<CalendarEvent> listar();
        List<CalendarEvent> filtrar(string id);
        void agregar(CalendarEvent ocalendar);
        void editar(CalendarEvent calendar);
        void eliminar(int idcalendar);
    }
}
