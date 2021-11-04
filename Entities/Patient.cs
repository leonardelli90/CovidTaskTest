using System;

namespace CovidTaskTest.Entities
{
    public record Patient
    {
        public Guid Id { get; init; }
        public int Status { get; init; }
        public String Name { get; init; }
        public String Surname { get; init; }
        public DateTimeOffset CreatedOn { get; init; }
        public DateTimeOffset ModifiedOn { get; init; }

    }
}