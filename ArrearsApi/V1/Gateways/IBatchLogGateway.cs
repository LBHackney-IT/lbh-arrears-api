using System.Collections.Generic;
using System.Threading.Tasks;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Gateways
{
    public interface IBatchLogGateway
    {
        Task<BatchLog> GetEntityByIdAsync(long id);

        Task<List<BatchLog>> GetAllAsync();
    }
}
