using PmsWebApi.DTOs;
using PmsWebApi.Models;

namespace PmsWebApi.Mappers
{
    public static class AppointmentMappers
    {
        public static Appointment toAppointment(this CreateAppointmentDTO dto)
        {
            return new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
            };
        }
    }
}
