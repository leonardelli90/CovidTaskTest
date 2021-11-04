using System;
using System.ComponentModel.DataAnnotations;

namespace CovidTaskTest.Dtos
{
    public record CreatePatientDto
    {
        [Required]
        public String Name { get; init; }
        
        [Required]
        public String Surname { get; init; }

        [Required]
        [Range(0, 2)]
        public int Status { get; init; }
        
    }
}