using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.DTO.Calendar
{
    public  class ListarCalendar
    {
        public int EventId { get; set; }

        public int? AsesorId { get; set; }

        public DateTime? EventStart { get; set; }

        public DateTime? EventEnd { get; set; }

        public string? EventDescription { get; set; }
    }
}
