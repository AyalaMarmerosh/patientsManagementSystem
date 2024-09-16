namespace PatientManagementApi.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppointmentDTO> Appointments { get; set; } 
    }
}
