using Amazon.DynamoDBv2.DataModel;
using Hackney.Core.DynamoDb.Converters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArrearsApi.V1.Infrastructure
{

    [Table("BatchLog")]
    public class BatchLogEntity
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public bool IsSuccess { get; set; }

    }
}
