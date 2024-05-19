using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proyect.EFCore.Repository;
using Proyect.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.EFCore
{
    public static class DependencyContainer
    {
        public static IServiceCollection DependencyEF(this IServiceCollection services)
        {
            services.AddScoped<ICalendarRepository, CalendarRepository>();

            services.AddScoped<IRegistroRepository, RegistroRepository>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();


            services.AddDbContext<SeguimientoCurricularContext>(optionsBuilder =>
            {
                optionsBuilder.
                UseSqlServer("server=DESKTOP-IVLDN19;database=SeguimientoCurricular;Integrated Security=True;Encrypt=False");
            });
            return services;
        }
    }
}
