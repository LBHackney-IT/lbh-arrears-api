using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ArrearsApi.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class BatchLogGateway : IBatchLogGateway
    {
        private readonly InterimSolutionContext _databaseContext;

        public BatchLogGateway(InterimSolutionContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BatchLog> GetEntityByIdAsync(long id)
        {
            var result = await _databaseContext.BatchLogEntities.FirstOrDefaultAsync(item => item.Id == id).ConfigureAwait(false);

            return result?.ToDomain();
        }

        public async Task<List<BatchLog>> GetAllAsync()
        {
            var result = await _databaseContext.BatchLogEntities.ToListAsync().ConfigureAwait(false);

            return result?.ToDomainList();
        }
    }
}
