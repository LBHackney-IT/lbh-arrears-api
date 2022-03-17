using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{
    public class GetAllBatchLogUseCase : IGetAllBatchLogUseCase
    {
        private readonly IBatchLogGateway _gateway;
        public GetAllBatchLogUseCase(IBatchLogGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<BatchLogResponse>> ExecuteAsync()
        {
            var batchLog = await _gateway.GetAllAsync().ConfigureAwait(false);

            if (batchLog == null)
                return null;

            return batchLog.ToResponse();
        }
    }
}
