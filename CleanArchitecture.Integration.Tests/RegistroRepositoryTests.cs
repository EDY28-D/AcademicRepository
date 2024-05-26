using Xunit;
using Microsoft.EntityFrameworkCore;
using Proyect.EFCore;
using Proyect.EFCore.Repository;
using Proyect.Entities.POCOS;
using System.Linq;

public class RegistroRepositoryTests
{
    private DbContextOptions<SeguimientoCurricularContext> _options;

    public RegistroRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<SeguimientoCurricularContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public void CanAddAndRetrieveRegistroAsesor()
    {
        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = new RegistroAsesor
            {
                AsesorId = 0,
                FullName = "Juan Perez",
                Email = "juan.perez@example.com",
                PhoneNumber = "123456789",
                Speciality = "Matemáticas",
                Role = "Asesor",
                Bhabilitado = 1
            };
            repository.agregar(registro);
            context.SaveChanges();
        }

        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = repository.listar().FirstOrDefault();
            Assert.NotNull(registro);
            Assert.Equal("Juan Perez", registro.FullName);
            Assert.Equal("juan.perez@example.com", registro.Email);
        }
    }

    [Fact]
    public void CanDeleteRegistroAsesor()
    {
        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = new RegistroAsesor
            {
                AsesorId = 0,
                FullName = "Juan Perez",
                Email = "juan.perez@example.com",
                PhoneNumber = "123456789",
                Speciality = "Matemáticas",
                Role = "Asesor",
                Bhabilitado = 1
            };
            repository.agregar(registro);
            context.SaveChanges();
        }

        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = repository.listar().FirstOrDefault();
            repository.eliminar(registro.AsesorId);
            context.SaveChanges();
        }

        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registros = repository.listar();
            Assert.Empty(registros);
        }
    }

    [Fact]
    public void CanUpdateRegistroAsesor()
    {
        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = new RegistroAsesor
            {
                AsesorId = 0,
                FullName = "Juan Perez",
                Email = "juan.perez@example.com",
                PhoneNumber = "123456789",
                Speciality = "Matemáticas",
                Role = "Asesor",
                Bhabilitado = 1
            };
            repository.agregar(registro);
            context.SaveChanges();
        }

        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = repository.listar().FirstOrDefault();
            registro.FullName = "Juan Actualizado";
            repository.editar(registro);
            context.SaveChanges();
        }

        using (var context = new SeguimientoCurricularContext(_options))
        {
            var repository = new RegistroRepository(context);
            var registro = repository.listar().FirstOrDefault();
            Assert.NotNull(registro);
            Assert.Equal("Juan Actualizado", registro.FullName);
        }
    }
}
