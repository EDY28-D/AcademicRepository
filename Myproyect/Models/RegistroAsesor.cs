using System;
using System.Collections.Generic;

namespace Myproyect.Models;

public partial class RegistroAsesor
{
    public int AsesorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Speciality { get; set; }

    public string? Role { get; set; }
}
