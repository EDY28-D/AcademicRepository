using Proyect.Entities.POCOS;
using Xunit;

public class RegistroAsesorTests
{
    [Fact]
    public void UpdateEmail_ShouldUpdateEmailWhenValid()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };
        var newEmail = "nuevo.email@example.com";

        // Act
        registro.UpdateEmail(newEmail);

        // Assert
        Assert.Equal(newEmail, registro.Email);
    }

    [Fact]
    public void UpdateEmail_ShouldThrowArgumentExceptionWhenEmailIsInvalid()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => registro.UpdateEmail("invalid-email"));
        Assert.Throws<ArgumentException>(() => registro.UpdateEmail(""));
    }

    [Fact]
    public void IsValid_ShouldReturnTrueWhenAllFieldsAreValid()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act
        var result = registro.IsValid();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseWhenFullNameIsMissing()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act
        var result = registro.IsValid();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseWhenEmailIsMissing()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act
        var result = registro.IsValid();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseWhenPhoneNumberIsMissing()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            Speciality = "Matemáticas",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act
        var result = registro.IsValid();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseWhenSpecialityIsMissing()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Role = "Asesor",
            Bhabilitado = 1
        };

        // Act
        var result = registro.IsValid();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseWhenRoleIsMissing()
    {
        // Arrange
        var registro = new RegistroAsesor
        {
            AsesorId = 1,
            FullName = "Juan Perez",
            Email = "juan.perez@example.com",
            PhoneNumber = "123456789",
            Speciality = "Matemáticas",
            Bhabilitado = 1
        };

        // Act
        var result = registro.IsValid();

        // Assert
        Assert.False(result);
    }
}
