using System.Collections.Generic;
using System.Linq;
using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Factories
{
    public static class ResponseFactory
    {
        public static BatchLogResponse ToResponse(this BatchLog batchLog)
        {
            return new BatchLogResponse
            {
                Id = batchLog.Id,
                Type = batchLog.Type,
                StartTime = batchLog.StartTime,
                EndTime = batchLog.EndTime,
                IsSuccess = batchLog.IsSuccess
            };
        }

        public static List<BatchLogResponse> ToResponse(this IEnumerable<BatchLog> batchLogList)
        {
            return batchLogList.Select(x => x.ToResponse()).ToList();
        }

        public static EvictionsResponse ToResponse(this Evictions evictions)
        {
            return new EvictionsResponse
            {
                Id = evictions.Id,
                TenancyRef = evictions.TenancyRef,
                Date = evictions.Date,
                CreatedAt = evictions.CreatedAt,
                UpdatedAt = evictions.UpdatedAt
            };
        }

        public static List<EvictionsResponse> ToResponse(this IEnumerable<Evictions> evictionsList)
        {
            return evictionsList.Select(x => x.ToResponse()).ToList();
        }
    }
}
