namespace PatientManagementApi.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AppointmentTypeDescription { get; set; } 
    }
}
