using System.ComponentModel.DataAnnotations;

namespace PmsWebApi.DTOs
{
    public class CreateAppointmentDTO
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
    }
}
