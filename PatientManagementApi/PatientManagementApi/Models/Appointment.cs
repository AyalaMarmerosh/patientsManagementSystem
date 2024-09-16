namespace PatientManagementApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public int PatientId { get; set;}
    }
    }

