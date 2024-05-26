using Moq;
using Xunit;
using Proyect.Controllers;
using Proyect.Entities.Interfaces;
using Proyect.Entities.POCOS;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;


    public class DeleteAsesorTests
    {
        [Fact]
        public void EliminarRegistroAsesor_ShouldReturnOkIfRegistroIsDeleted()
        {
            // Arrange
            var mockRegistroRepository = new Mock<IRegistroRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
            var controller = new RegistroAsesorController(mockRegistroRepository.Object, mockUnitOfWork.Object);

            int registroId = 1;
            var registro = new RegistroAsesor
            {
                AsesorId = registroId,
                FullName = "Juan Perez",
                Email = "juan.perez@example.com",
                PhoneNumber = "123456789",
                Speciality = "Matemáticas",
                Role = "Asesor",
                Bhabilitado = 1
            };

            mockRegistroRepository.Setup(repo => repo.listar()).Returns(new List<RegistroAsesor> { registro });
            mockRegistroRepository.Setup(repo => repo.eliminar(registroId)).Verifiable();
            mockUnitOfWork.Setup(u => u.SaveChanges()).Verifiable();

            // Act
            var result = controller.EliminarRegistroAsesor(registroId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Registro eliminado con éxito", actionResult.Value);

            mockRegistroRepository.Verify(repo => repo.eliminar(registroId), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EliminarRegistroAsesor_ShouldReturnNotFoundIfRegistroDoesNotExist()
        {
            // Arrange
            var mockRegistroRepository = new Mock<IRegistroRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
            var controller = new RegistroAsesorController(mockRegistroRepository.Object, mockUnitOfWork.Object);

            int registroId = 1;
            mockRegistroRepository.Setup(repo => repo.listar()).Returns(new List<RegistroAsesor>());

            // Act
            var result = controller.EliminarRegistroAsesor(registroId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Registro no encontrado.", actionResult.Value);

            mockRegistroRepository.Verify(repo => repo.eliminar(It.IsAny<int>()), Times.Never);
            mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }

        [Fact]
        public void EliminarRegistroAsesor_ShouldReturnBadRequestOnException()
        {
            // Arrange
            var mockRegistroRepository = new Mock<IRegistroRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
            var controller = new RegistroAsesorController(mockRegistroRepository.Object, mockUnitOfWork.Object);

            int registroId = 1;
            var registro = new RegistroAsesor
            {
                AsesorId = registroId,
                FullName = "Juan Perez",
                Email = "juan.perez@example.com",
                PhoneNumber = "123456789",
                Speciality = "Matemáticas",
                Role = "Asesor",
                Bhabilitado = 1
            };

            mockRegistroRepository.Setup(repo => repo.listar()).Returns(new List<RegistroAsesor> { registro });
            mockRegistroRepository.Setup(repo => repo.eliminar(registroId)).Throws(new Exception("Error de prueba"));

            // Act
            var result = controller.EliminarRegistroAsesor(registroId);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error al eliminar: Error de prueba", actionResult.Value);

            mockRegistroRepository.Verify(repo => repo.eliminar(registroId), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

