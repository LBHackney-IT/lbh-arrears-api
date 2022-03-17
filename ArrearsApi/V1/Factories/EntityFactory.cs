using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ArrearsApi.V1.Factories
{
    public static class EntityFactory
    {
        public static BatchLog ToDomain(this BatchLogEntity batchLogEntity)
        {
            return new BatchLog
            {
                Id = batchLogEntity.Id,
                Type = batchLogEntity.Type,
                StartTime = batchLogEntity.StartTime,
                EndTime = batchLogEntity.EndTime,
                IsSuccess = batchLogEntity.IsSuccess
            };
        }

        public static BatchLogEntity ToDatabase(this BatchLog batchLog)
        {
            return new BatchLogEntity
            {
                Id = batchLog.Id,
                Type = batchLog.Type,
                StartTime = batchLog.StartTime,
                EndTime = batchLog.EndTime,
                IsSuccess = batchLog.IsSuccess
            };
        }

        public static List<BatchLog> ToDomainList(this IEnumerable<BatchLogEntity> batchLogEntityList)
        {
            return batchLogEntityList.Select(x => x.ToDomain()).ToList();
        }

        public static Evictions ToDomain(this EvictionsEntity evictionsEntity)
        {
            return new Evictions
            {
                Id = evictionsEntity.Id,
                TenancyRef = evictionsEntity.TenancyRef,
                Date = evictionsEntity.Date,
                CreatedAt = evictionsEntity.CreatedAt,
                UpdatedAt = evictionsEntity.UpdatedAt
            };
        }

        public static EvictionsEntity ToDatabase(this Evictions evictions)
        {
            return new EvictionsEntity
            {
                Id = evictions.Id,
                TenancyRef = evictions.TenancyRef,
                Date = evictions.Date,
                CreatedAt = evictions.CreatedAt,
                UpdatedAt = evictions.UpdatedAt
            };
        }

        public static List<Evictions> ToDomainList(this IEnumerable<EvictionsEntity> evictionsEntityList)
        {
            return evictionsEntityList.Select(x => x.ToDomain()).ToList();
        }
    }
}
