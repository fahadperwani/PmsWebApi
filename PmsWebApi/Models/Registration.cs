using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PmsWebApi.Models
{
    public class Registration
    {
        public string Name { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Boolean isDoctor { get; set; }
        public string? Specialization { get; set; }

    }
}
