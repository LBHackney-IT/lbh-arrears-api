using ArrearsApi.V1.Boundary.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase.Interfaces
{
    public interface IGetAllBatchLogUseCase
    {
        Task<List<BatchLogResponse>> ExecuteAsync();
    }
}
