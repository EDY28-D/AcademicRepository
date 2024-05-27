using Microsoft.EntityFrameworkCore;
using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using Proyect.EFCore.Repository;
using System;
using System.Linq;
using Xunit;
using Proyect.EFCore;

public class RegistroRepositoryTests
{
    private DbContextOptions<SeguimientoCurricularContext> dbContextOptions;

    public RegistroRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<SeguimientoCurricularContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public void Agregar_ValidAsesor_ShouldAddToDatabase()
    {
        using (var context = new SeguimientoCurricularContext(dbContextOptions))
        {
            var repo = new RegistroRepository(context);
            var asesor = new RegistroAsesor
            {
                FullName = "Juan Perez",
                Email = "juan@example.com",
                PhoneNumber = "123456789",
                Speciality = "Math",
                Role = "Teacher",
                Bhabilitado = 1
            };

            repo.agregar(asesor);
            context.SaveChanges();

            var addedAsesor = context.RegistroAsesors.FirstOrDefault(a => a.Email == "juan@example.com");
            Assert.NotNull(addedAsesor);
        }
    }

    [Fact]
    public void Editar_ValidAsesor_ShouldUpdateInDatabase()
    {
        using (var context = new SeguimientoCurricularContext(dbContextOptions))
        {
            var repo = new RegistroRepository(context);
            var asesor = new RegistroAsesor
            {
                AsesorId = 1,
                FullName = "Juan Perez",
                Email = "juan@example.com",
                PhoneNumber = "123456789",
                Speciality = "Math",
                Role = "Teacher",
                Bhabilitado = 1
            };
            context.RegistroAsesors.Add(asesor);
            context.SaveChanges();

            asesor.FullName = "Juan P. Perez";
            repo.editar(asesor);
            context.SaveChanges();

            var updatedAsesor = context.RegistroAsesors.FirstOrDefault(a => a.AsesorId == 1);
            Assert.Equal("Juan P. Perez", updatedAsesor.FullName);
        }
    }

    [Fact]
    public void Eliminar_ValidId_ShouldDisableInDatabase()
    {
        using (var context = new SeguimientoCurricularContext(dbContextOptions))
        {
            var repo = new RegistroRepository(context);
            var asesor = new RegistroAsesor
            {
                AsesorId = 1,
                FullName = "Juan Perez",
                Email = "juan@example.com",
                PhoneNumber = "123456789",
                Speciality = "Math",
                Role = "Teacher",
                Bhabilitado = 1
            };
            context.RegistroAsesors.Add(asesor);
            context.SaveChanges();

            repo.eliminar(1);
            context.SaveChanges();

            var deletedAsesor = context.RegistroAsesors.FirstOrDefault(a => a.AsesorId == 1);
            Assert.Equal(0, deletedAsesor.Bhabilitado);
        }
    }

    [Fact]
    public void EmailExiste_ExistingEmail_ShouldReturnTrue()
    {
        using (var context = new SeguimientoCurricularContext(dbContextOptions))
        {
            var repo = new RegistroRepository(context);
            var asesor = new RegistroAsesor
            {
                FullName = "Juan Perez",
                Email = "juan@example.com",
                PhoneNumber = "123456789",
                Speciality = "Math",
                Role = "Teacher",
                Bhabilitado = 1
            };
            context.RegistroAsesors.Add(asesor);
            context.SaveChanges();

            var result = repo.EmailExiste("juan@example.com");

            Assert.True(result);
        }
    }

    [Fact]
    public void EmailExiste_NonExistingEmail_ShouldReturnFalse()
    {
        using (var context = new SeguimientoCurricularContext(dbContextOptions))
        {
            var repo = new RegistroRepository(context);
            var result = repo.EmailExiste("nonexistent@example.com");

            Assert.False(result);
        }
    }
}
