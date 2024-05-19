using Proyect.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.EFCore.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public SeguimientoCurricularContext context;

        public UnitOfWorkRepository(SeguimientoCurricularContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
