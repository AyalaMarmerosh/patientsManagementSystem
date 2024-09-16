namespace PatientManagementApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
