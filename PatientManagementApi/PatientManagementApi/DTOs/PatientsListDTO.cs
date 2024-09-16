namespace PatientManagementApi.DTOs
{
    public class PatientsListDTO
    {
        public List<PatientDTO> Patients { get; set; }
        public int TotalPatients { get; set; } 
    }
}
