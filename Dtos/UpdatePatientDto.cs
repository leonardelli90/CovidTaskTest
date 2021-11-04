using System;
using System.ComponentModel.DataAnnotations;

namespace CovidTaskTest.Dtos
{
    public record UpdatePatientDto
    {
        [Required]
        public String Name { get; init; }

        [Required]
        public String Surname { get; init; }
        [Required]
        public int Status { get; init;}
        
    }
}