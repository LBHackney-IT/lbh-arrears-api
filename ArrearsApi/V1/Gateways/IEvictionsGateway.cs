using System.Collections.Generic;
using System.Threading.Tasks;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Gateways
{
    public interface IEvictionsGateway
    {
        Task<Evictions> GetEntityByIdAsync(int id);

        Task<List<Evictions>> GetAllAsync();
    }
}
