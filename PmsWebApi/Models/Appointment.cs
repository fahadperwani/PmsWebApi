using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsWebApi.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string PatientId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string DoctorId { get; set; }

        public User? Doctor { get; set; }

        public User? Patient { get; set; }

        [Required]
        [GreaterThanToday]
        public DateTime Time { get; set; }


        public class GreaterThanTodayAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value == null) return true;

                DateTime date = (DateTime)value;
                return date.Date == DateTime.Today && date.TimeOfDay > DateTime.Now.TimeOfDay;
            }
        }

    }
}
