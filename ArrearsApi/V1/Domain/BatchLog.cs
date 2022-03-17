using System;

namespace ArrearsApi.V1.Domain
{
    public class BatchLog
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool IsSuccess { get; set; }
    }
}
