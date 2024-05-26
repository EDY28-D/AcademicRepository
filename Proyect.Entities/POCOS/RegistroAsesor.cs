using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyect.Entities.POCOS;

public partial class RegistroAsesor
{
    public int AsesorId { get; set; }

    [Required(ErrorMessage = "The FullName field is required.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The PhoneNumber field is required.")]
    [Phone(ErrorMessage = "The PhoneNumber field is not a valid phone number.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The Speciality field is required.")]
    public string Speciality { get; set; }

    [Required(ErrorMessage = "The Role field is required.")]
    public string Role { get; set; }

    public int Bhabilitado { get; set; }

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrEmpty(newEmail) || !newEmail.Contains("@"))
        {
            throw new ArgumentException("Email inválido");
        }
        Email = newEmail;
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(FullName) &&
               !string.IsNullOrEmpty(Email) &&
               !string.IsNullOrEmpty(PhoneNumber) &&
               !string.IsNullOrEmpty(Speciality) &&
               !string.IsNullOrEmpty(Role);
    }
}
