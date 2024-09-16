using PatientManagementApi.DTOs;

namespace PatientManagementApi.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDTO>> GetPatientsAsync(int page, int pageSize);
        Task<IEnumerable<PatientDTO>> SearchPatientsAsync(string query);
    }
}
