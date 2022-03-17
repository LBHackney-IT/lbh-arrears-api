using ArrearsApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase.Interfaces
{
    public interface IGetEvictionsByIdUseCase
    {
        Task<EvictionsResponse> ExecuteAsync(int id);
    }
}
