using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{
    public class GetAllEvictionsUseCase : IGetAllEvictionsUseCase
    {
        private readonly IEvictionsGateway _gateway;
        public GetAllEvictionsUseCase(IEvictionsGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<EvictionsResponse>> ExecuteAsync()
        {
            var evictions = await _gateway.GetAllAsync().ConfigureAwait(false);

            if (evictions == null)
                return null;

            return evictions.ToResponse();
        }
    }
}
