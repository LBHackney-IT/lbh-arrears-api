using ArrearsApi.V1.Boundary.Response;

namespace ArrearsApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResponseObject Execute(int id);
    }
}
