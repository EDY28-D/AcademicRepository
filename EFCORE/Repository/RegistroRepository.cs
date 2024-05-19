using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.EFCore.Repository
{
    public class RegistroRepository: IRegistroRepository
    {
        private readonly SeguimientoCurricularContext _bd;
        public RegistroRepository(SeguimientoCurricularContext bd)

        {
            this._bd = bd;
        }

        public void agregar(RegistroAsesor oRegistroAsesor)
        {
            _bd.RegistroAsesors.Add(oRegistroAsesor);
        }

        public void editar(RegistroAsesor oRegistroAsesor)
        {
            var obj= _bd.RegistroAsesors.Where(P=>P.AsesorId== oRegistroAsesor.AsesorId).FirstOrDefault();
            if (obj !=null)
            {
                obj.FullName = oRegistroAsesor.FullName;
                obj.PhoneNumber = oRegistroAsesor.PhoneNumber;
                obj.Email = oRegistroAsesor.Email;
                obj.Speciality = oRegistroAsesor.Speciality;
                obj.Role = oRegistroAsesor.Role;
            }
        }

        public void eliminar(int idRegistroAsesor)
        {
            var obj = _bd.RegistroAsesors.Where(P => P.AsesorId == idRegistroAsesor).FirstOrDefault();
            if (obj != null)
            {
                obj.Bhabilitado = 0;
                //_bd.RegistroAsesors.Remove(obj);
                //_bd.SaveChanges();
            }
        }

        public List<RegistroAsesor> filtrar(string nombre)
        {
            return _bd.RegistroAsesors.Where(p => p.FullName.Contains(nombre)
            && p.Bhabilitado==1).ToList();
        }

        public List<RegistroAsesor> listar()
        {
            return _bd.RegistroAsesors.Where(p=>p.Bhabilitado == 1).ToList();
        }
    }
}
