using Moq;
using Xunit;
using Proyect.Controllers;
using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class RegistroAsesorControllerTests
{
    [Fact]
    public void GuardarRegistro_ShouldReturnBadRequestIfEmailExists()
    {
        // Arrange
        var mockRegistroRepository = new Mock<IRegistroRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
        var controller = new RegistroAsesorController(mockRegistroRepository.Object, mockUnitOfWork.Object);

        var nuevoRegistro = new RegistroAsesor
        {
            AsesorId = 0,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        mockRegistroRepository.Setup(repo => repo.EmailExiste(nuevoRegistro.Email)).Returns(true);

        // Act
        var result = controller.GuardarRegistro(nuevoRegistro);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("El correo electrónico ya existe.", actionResult.Value);

        mockRegistroRepository.Verify(repo => repo.agregar(It.IsAny<RegistroAsesor>()), Times.Never);
        mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GuardarRegistro_ShouldReturnBadRequestIfModelStateIsInvalid()
    {
        // Arrange
        var mockRegistroRepository = new Mock<IRegistroRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
        var controller = new RegistroAsesorController(mockRegistroRepository.Object, mockUnitOfWork.Object);
        controller.ModelState.AddModelError("FullName", "The FullName field is required.");

        var nuevoRegistro = new RegistroAsesor
        {
            AsesorId = 0,
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act
        var result = controller.GuardarRegistro(nuevoRegistro);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = actionResult as BadRequestObjectResult;
        var errorResponse = badRequestResult.Value as SerializableError;
        Assert.Contains("FullName", errorResponse.Keys);
    }
}
