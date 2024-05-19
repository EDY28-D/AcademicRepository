using System;
using System.Collections.Generic;

namespace Proyect.Entities.POCOS;

public partial class CalendarEvent
{
    public int EventId { get; set; }

    public int? AsesorId { get; set; }

    public DateTime? EventStart { get; set; }

    public DateTime? EventEnd { get; set; }

    public string? EventDescription { get; set; }

    public string? StudentEmail { get; set; }

    public bool? IsAvailable { get; set; }
    public int? Bhabilitado { get; set; }
}
