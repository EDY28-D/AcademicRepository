using Proyect.Entities.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Entities.Interfaces
{
    public interface IRegistroRepository
    {
        List<RegistroAsesor> listar();
        List<RegistroAsesor> filtrar(string nombre);
        void agregar(RegistroAsesor oRegistroAsesor);
        void editar(RegistroAsesor registroAsesor);
        void eliminar(int idRegistroAsesor);
        bool EmailExiste(string email);

    }
}
