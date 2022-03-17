using System;

namespace ArrearsApi.V1.Boundary.Response
{
    //TODO: Rename to represent to object you will be returning eg. ResidentInformation, HouseholdDetails e.t.c
    public class EvictionsResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TenancyRef { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
