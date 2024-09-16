using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.DTOs;
using PatientManagementApi.Interfaces;
using PatientManagementApi.Models;

namespace PatientManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;


        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var patients = await _patientService.GetPatientsAsync(page, pageSize);
            var totalPatients = (await _patientService.GetPatientsAsync(1, int.MaxValue)).Count();
           
            return Ok(new PatientsListDTO{ Patients = patients.ToList(), TotalPatients = totalPatients});
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPatients([FromQuery] string query)
        {
            var patients = await _patientService.SearchPatientsAsync(query);
           
            return Ok(patients.ToList());
        }
    }
}

