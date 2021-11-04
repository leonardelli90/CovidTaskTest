using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidTaskTest.Dtos;
using CovidTaskTest.Entities;
using CovidTaskTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CovidTaskTest.Controllers
{

    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;

        public PatientsController(IPatientsRepository patientsRepository) 
        {
            _patientsRepository = patientsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PatientDto>> GetPatientsAsync()
        {
            var patient = (await _patientsRepository.GetPatientsAsync())
                            .Select( patient => patient.AsDto());
            return patient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientAsync(Guid id)
        {
            var patient = await _patientsRepository.GetPatientAsync(id);

            if (patient is null) {
                return NotFound();
            }

            return patient.AsDto();
        }

        [HttpGet("filter")]
        public async Task<IEnumerable<PatientDto>> GetFilterdPatientsAsync(string timestamp, string status)
        {
            var filteredPatients = (await _patientsRepository.GetFilterdPatientsAsync(timestamp, status))
                        .Select(patient => patient.AsDto());

            return filteredPatients;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatientAsync(CreatePatientDto patientDto)
        {

            Patient patient = new(){
                Id = Guid.NewGuid(),
                Status = patientDto.Status,
                Name = patientDto.Name,
                Surname = patientDto.Surname,
                CreatedOn = DateTimeOffset.UtcNow,
                ModifiedOn = DateTimeOffset.UtcNow
            };

            await _patientsRepository.CreatePatientAsync(patient);

            return CreatedAtAction(nameof(GetPatientAsync), new { id = patient.Id}, patient.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatientAsync(Guid id, UpdatePatientDto patientDto) 
        {
            var existingPatient = await _patientsRepository.GetPatientAsync(id);

            if (existingPatient is null)
            {
                return NotFound();
            }

            Patient updatedPatient = existingPatient with {
                Name = patientDto.Name,
                Surname = patientDto.Surname,
                Status = patientDto.Status,
                ModifiedOn = DateTimeOffset.UtcNow
            };

            await _patientsRepository.UpdatePatientAsync(updatedPatient);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatientAsync(Guid id)
        {
             var existingItem = await _patientsRepository.GetPatientAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await _patientsRepository.DeletePatientAsync(id);

            return NoContent();
        }
    }
}