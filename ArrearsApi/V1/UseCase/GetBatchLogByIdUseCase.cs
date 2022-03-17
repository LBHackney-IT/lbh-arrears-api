using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{   
    public class GetBatchLogByIdUseCase : IGetBatchLogByIdUseCase
    {
        private IBatchLogGateway _gateway;
        public GetBatchLogByIdUseCase(IBatchLogGateway gateway)
        {
            _gateway = gateway;
        }
        
        public async Task<BatchLogResponse> ExecuteAsync(long id)
        {
            var batchLog = await _gateway.GetEntityByIdAsync(id).ConfigureAwait(false);

            if (batchLog == null)
                return null;

            return batchLog.ToResponse();
        }
    }
}
