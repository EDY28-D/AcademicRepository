using Microsoft.AspNetCore.Mvc;
using Myproyect.Models;

namespace Myproyect.Controllers
{
    public class RegistroAsesorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<RegistroAsesor> listar()
        {
            using(SeguimientoCurricularContext bd=new SeguimientoCurricularContext())
            {
                return bd.RegistroAsesors.ToList();
            }
        }
        public List<RegistroAsesor> filtrar(string nombre)
        {
            using (SeguimientoCurricularContext bd = new SeguimientoCurricularContext())
            {
                return bd.RegistroAsesors.Where(p=>p.FullName.Contains(nombre)).ToList(); 
            }
        }
    }
}
