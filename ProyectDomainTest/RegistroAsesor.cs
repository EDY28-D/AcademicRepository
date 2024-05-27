using Proyect.Entities.POCOS;
using System;
using Xunit;

public class RegistroAsesorTests
{
    [Fact]
    public void UpdateEmail_ValidEmail_ShouldUpdateEmail()
    {
        var asesor = new RegistroAsesor { Email = "old@example.com" };
        var newEmail = "new@example.com";

        asesor.UpdateEmail(newEmail);

        Assert.Equal(newEmail, asesor.Email);
    }

    [Fact]
    public void UpdateEmail_InvalidEmail_ShouldThrowArgumentException()
    {
        var asesor = new RegistroAsesor { Email = "old@example.com" };

        Assert.Throws<ArgumentException>(() => asesor.UpdateEmail("invalid-email"));
    }

    [Fact]
    public void IsValid_ValidAsesor_ShouldReturnTrue()
    {
        var asesor = new RegistroAsesor
        {
            FullName = "Juan Perez",
            Email = "juan@example.com",
            PhoneNumber = "123456789",
            Speciality = "Math",
            Role = "Teacher"
        };

        Assert.True(asesor.IsValid());
    }

    [Fact]
    public void IsValid_InvalidAsesor_ShouldReturnFalse()
    {
        var asesor = new RegistroAsesor
        {
            FullName = "Juan Perez",
            Email = "",
            PhoneNumber = "123456789",
            Speciality = "Math",
            Role = "Teacher"
        };

        Assert.False(asesor.IsValid());
    }
}
