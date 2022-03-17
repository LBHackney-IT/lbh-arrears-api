using AutoFixture;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Infrastructure;

namespace ArrearsApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static EvictionsEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<ArrearsApi.V1.Domain.BatchLog>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static EvictionsEntity CreateDatabaseEntityFrom(ArrearsApi.V1.Domain.BatchLog entity)
        {
            return new EvictionsEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
            };
        }
    }
}
