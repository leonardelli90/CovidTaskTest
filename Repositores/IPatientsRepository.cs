using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CovidTaskTest.Entities;

namespace CovidTaskTest.Repositories
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientAsync(Guid id);
        Task<IEnumerable<Patient>> GetFilterdPatientsAsync(String timestamp, String status);
        Task CreatePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(Guid id);
    }

}