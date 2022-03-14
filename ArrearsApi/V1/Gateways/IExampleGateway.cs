using System.Collections.Generic;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
