using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.DTO.RegistroAsesor
{
    public class ListarRegistroAsesorDTO
    {
        public int IdAsesor { get; set; }
        public string nombre { get; set; }
        public string? Especialidad { get; set; }
        public int? Habilitado { get; set; }
    }


}
