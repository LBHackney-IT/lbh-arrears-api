using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{   
    public class GetEvictionsByIdUseCase : IGetEvictionsByIdUseCase
    {
        private IEvictionsGateway _gateway;
        public GetEvictionsByIdUseCase(IEvictionsGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<EvictionsResponse> ExecuteAsync(int id)
        {
            var evictions = await _gateway.GetEntityByIdAsync(id).ConfigureAwait(false);

            if (evictions == null)
                return null;

            return evictions.ToResponse();
        }
    }
}
