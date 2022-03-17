using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ArrearsApi.V1.Gateways
{   
    public class EvictionsGateway : IEvictionsGateway
    {
        private readonly IncomeContext _databaseContext;

        public EvictionsGateway(IncomeContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Evictions> GetEntityByIdAsync(int id)
        {
            var result = await _databaseContext.EvictionsEntities.FirstOrDefaultAsync(item => item.Id == id).ConfigureAwait(false);

            return result?.ToDomain();
        }

        public async Task<List<Evictions>> GetAllAsync()
        {
            var result = await _databaseContext.EvictionsEntities.ToListAsync().ConfigureAwait(false);

            return result?.ToDomainList();
        }
    }
}
