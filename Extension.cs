using CovidTaskTest.Dtos;
using CovidTaskTest.Entities;

namespace CovidTaskTest 
{
    public static class Extension 
    {
        public static PatientDto AsDto(this Patient patient)
        {
            return new PatientDto{
                Id = patient.Id,
                Status = patient.Status,
                Name = patient.Name,
                Surname = patient.Surname,
                CreatedOn = patient.CreatedOn,
                ModifiedOn = patient.ModifiedOn
            };
        }
    }    
}