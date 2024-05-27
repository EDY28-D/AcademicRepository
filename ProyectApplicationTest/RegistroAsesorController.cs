using Microsoft.AspNetCore.Mvc;
using Moq;
using Proyect.Controllers;
using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using Proyect.DTO.RegistroAsesor;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class RegistroAsesorControllerTests
{
    private readonly Mock<IRegistroRepository> _mockRepo;
    private readonly Mock<IUnitOfWorkRepository> _mockUnit;
    private readonly RegistroAsesorController _controller;

    public RegistroAsesorControllerTests()
    {
        _mockRepo = new Mock<IRegistroRepository>();
        _mockUnit = new Mock<IUnitOfWorkRepository>();
        _controller = new RegistroAsesorController(_mockRepo.Object, _mockUnit.Object);
    }

    [Fact]
    public void Listar_ShouldReturnOkResultWithListOfAsesores()
    {
        _mockRepo.Setup(repo => repo.listar()).Returns(GetSampleAsesores());
        var result = _controller.Listar();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var asesores = Assert.IsType<List<ListarRegistroAsesorDTO>>(okResult.Value);
        Assert.Equal(3, asesores.Count);
    }

    [Fact]
    public void Filtrar_ExistingName_ShouldReturnOkResult()
    {
        string nombre = "Juan";
        _mockRepo.Setup(repo => repo.filtrar(nombre)).Returns(GetSampleAsesores().Where(a => a.FullName.Contains(nombre)).ToList());
        var result = _controller.Filtrar(nombre);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var asesores = Assert.IsType<List<ListarRegistroAsesorDTO>>(okResult.Value);
        Assert.Single(asesores);
    }

    [Fact]
    public void EliminarRegistroAsesor_ValidId_ShouldReturnOkResult()
    {
        int id = 1;
        _mockRepo.Setup(repo => repo.listar()).Returns(GetSampleAsesores());
        _mockRepo.Setup(repo => repo.eliminar(id));
        _mockUnit.Setup(unit => unit.SaveChanges()).Returns(1);

        var result = _controller.EliminarRegistroAsesor(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void EliminarRegistroAsesor_InvalidId_ShouldReturnNotFoundResult()
    {
        int id = 99;
        _mockRepo.Setup(repo => repo.listar()).Returns(GetSampleAsesores());

        var result = _controller.EliminarRegistroAsesor(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GuardarRegistro_ValidAsesor_ShouldReturnOkResult()
    {
        var asesor = new RegistroAsesor
        {
            FullName = "Juan Perez",
            Email = "juan@example.com",
            PhoneNumber = "123456789",
            Speciality = "Math",
            Role = "Teacher",
            Bhabilitado = 1
        };

        _mockRepo.Setup(repo => repo.EmailExiste(asesor.Email)).Returns(false);
        _mockRepo.Setup(repo => repo.agregar(asesor));
        _mockUnit.Setup(unit => unit.SaveChanges()).Returns(1);

        var result = _controller.GuardarRegistro(asesor);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GuardarRegistro_InvalidModel_ShouldReturnBadRequest()
    {
        var asesor = new RegistroAsesor(); // Invalid model

        _controller.ModelState.AddModelError("FullName", "Required");

        var result = _controller.GuardarRegistro(asesor);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    private List<RegistroAsesor> GetSampleAsesores()
    {
        return new List<RegistroAsesor>
        {
            new RegistroAsesor { AsesorId = 1, FullName = "Juan Perez", Email = "juan@example.com", PhoneNumber = "123456789", Speciality = "Math", Role = "Teacher", Bhabilitado = 1 },
            new RegistroAsesor { AsesorId = 2, FullName = "Ana Gomez", Email = "ana@example.com", PhoneNumber = "987654321", Speciality = "Science", Role = "Teacher", Bhabilitado = 1 },
            new RegistroAsesor { AsesorId = 3, FullName = "Luis Sanchez", Email = "luis@example.com", PhoneNumber = "555555555", Speciality = "History", Role = "Teacher", Bhabilitado = 1 }
        };
    }
}
