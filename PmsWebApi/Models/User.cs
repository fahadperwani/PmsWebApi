using Microsoft.AspNetCore.Identity;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PmsWebApi.Models
{
    public class User: IdentityUser
    {

        [Required]
        [MaxLength(255)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        public Boolean isDoctor {  get; set; }
        public string? Specialization { get; set; }

        public ICollection<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
        public ICollection<Appointment> PatientAppointments { get; set; } = new List<Appointment>();
    }
}
