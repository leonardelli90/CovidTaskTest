using System;

namespace CovidTaskTest.Dtos
{
    public record PatientDto
    {
        public Guid Id { get; init; }
        public int Status { get; init; }
        public String Name { get; init; }
        public String Surname { get; init; }
        public DateTimeOffset CreatedOn { get; init; }
        public DateTimeOffset ModifiedOn { get; init; }

    }
}