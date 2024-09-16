using PatientManagementApi.DTOs;
using PatientManagementApi.Interfaces;
using PatientManagementApi.Models;

namespace PatientManagementApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly List<AppointmentType> _appointmentTypes;
        private readonly List<Patient> _patients;

        public PatientService()
        {
            _appointmentTypes = new List<AppointmentType>
            {
                new AppointmentType { Id = 1, Description = "ייעוץ" },
                new AppointmentType { Id = 2, Description = "סיכום" },
                new AppointmentType { Id = 3, Description = "בדיקה"}
            };
            _patients = new List<Patient>
            {
                new Patient { Id = 1, Name = "אברהם", Appointments= new List<Appointment>
                    {
                        new Appointment { Id = 1, Date = DateTime.Now.AddDays(1), AppointmentType = _appointmentTypes[0], PatientId = 1 },
                        new Appointment { Id = 2, Date = DateTime.Now.AddDays(3), AppointmentType = _appointmentTypes[1], PatientId = 1 },
                        new Appointment { Id = 3, Date = DateTime.Now.AddDays(-1), AppointmentType = _appointmentTypes[2], PatientId = 1 }

                    }
                },

                new Patient { Id = 2, Name = "יצחק", Appointments = new List<Appointment>
                    {
                        new Appointment { Id = 4, Date = DateTime.Now.AddDays(-3), AppointmentType = _appointmentTypes[1], PatientId = 2},
                        new Appointment { Id = 5, Date = DateTime.Now.AddDays(-2), AppointmentType = _appointmentTypes[2], PatientId = 2}
                    }
                },
                new Patient { Id = 3, Name = "יעקב", Appointments= new List<Appointment>
                    {

                    }
                },

                new Patient { Id = 4, Name = "משה", Appointments = new List<Appointment>
                    {

                    }
                },
                new Patient { Id = 5, Name = "אהרון", Appointments= new List<Appointment>
                    {
                        new Appointment { Id = 6, Date = DateTime.Now.AddDays(5), AppointmentType = _appointmentTypes[2], PatientId = 5 }
                    }
                },

                new Patient { Id = 6, Name = "דוד", Appointments = new List<Appointment>
                    {
                    new Appointment { Id = 7, Date = DateTime.Now.AddDays(-1), AppointmentType = _appointmentTypes[1], PatientId = 6}
                    }
                },
                new Patient { Id = 7, Name = "שרה", Appointments= new List<Appointment>
                    {
                        new Appointment { Id = 8, Date = DateTime.Now.AddDays(2), AppointmentType = _appointmentTypes[0], PatientId = 7 }
                    }
                },

                new Patient { Id = 8, Name = "רבקה", Appointments = new List<Appointment>
                    {
                    new Appointment { Id = 9, Date = DateTime.Now.AddDays(-1), AppointmentType = _appointmentTypes[1], PatientId = 8}
                    }
                },
                new Patient { Id = 9, Name = "רחל", Appointments= new List<Appointment>
                    {
                        new Appointment { Id = 10, Date = DateTime.Now.AddDays(7), AppointmentType = _appointmentTypes[2], PatientId = 9 }
                    }
                },

                new Patient { Id = 10, Name = "לאה", Appointments = new List<Appointment>
                    {
                    new Appointment { Id = 11, Date = DateTime.Now.AddDays(-7), AppointmentType = _appointmentTypes[1], PatientId = 10}
                    }
                },
                new Patient { Id = 11, Name = "יעל", Appointments= new List<Appointment>
                    {
                        new Appointment { Id = 12, Date = DateTime.Now.AddDays(10), AppointmentType = _appointmentTypes[0], PatientId = 11 }
                    }
                },

                new Patient { Id = 12, Name = "אסתר", Appointments = new List<Appointment>
                    {
                    new Appointment { Id = 13, Date = DateTime.Now.AddDays(-4), AppointmentType = _appointmentTypes[2], PatientId = 12}
                    }
                }
            };
        }
        public Task<IEnumerable<PatientDTO>> GetPatientsAsync(int page, int pageSize)
        {
            var patients = _patients
                .OrderBy(p => p.Appointments.FirstOrDefault(a => a.Date > DateTime.Now)?.Date ?? DateTime.MaxValue)
                .ThenByDescending(p => p.Appointments.FirstOrDefault(a => a.Date <= DateTime.Now)?.Date ?? DateTime.MinValue)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PatientDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Appointments = p.Appointments.Select(a => new AppointmentDTO
                    { 
                        Id= a.Id,
                        Date = a.Date,
                        AppointmentTypeDescription = a.AppointmentType.Description
                    }).ToList(),
                }).ToList();

            return Task.FromResult(patients.AsEnumerable());
        }

        public Task<IEnumerable<PatientDTO>> SearchPatientsAsync(string query)
        {
            var patients = _patients.Where(p =>
                p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Id.ToString() == query)
                .Select(p => new PatientDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Appointments = p.Appointments.Select(a => new AppointmentDTO
                    {
                        Id = a.Id,
                        Date = a.Date,
                        AppointmentTypeDescription= a.AppointmentType.Description
                    }).ToList()
                }).ToList();

            return Task.FromResult(patients.AsEnumerable());
        }
    }
}
