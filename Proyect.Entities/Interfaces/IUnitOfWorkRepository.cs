using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.Entities.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        int SaveChanges();
    }
}
