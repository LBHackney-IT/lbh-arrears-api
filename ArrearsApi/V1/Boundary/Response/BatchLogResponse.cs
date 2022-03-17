using System;

namespace ArrearsApi.V1.Boundary.Response
{
    //TODO: Rename to represent to object you will be returning eg. ResidentInformation, HouseholdDetails e.t.c
    public class BatchLogResponse
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool IsSuccess { get; set; }
    }
}
