using ArrearsApi.V1.Controllers;
using ArrearsApi.V1.UseCase.Interfaces;
using Hackney.Core.Testing.Shared;
using Moq;
using NUnit.Framework;

namespace ArrearsApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ArrearsApiControllerTests : LogCallAspectFixture
    {
        private TestController _classUnderTest;
        private Mock<IGetBatchLogByIdUseCase> _mockGetByIdUseCase;
        private Mock<IGetAllBatchLogUseCase> _mockGetByAllUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockGetByIdUseCase = new Mock<IGetBatchLogByIdUseCase>();
            _mockGetByAllUseCase = new Mock<IGetAllBatchLogUseCase>();
            _classUnderTest = new TestController(_mockGetByAllUseCase.Object, _mockGetByIdUseCase.Object);
        }


        //Add Tests Here
    }
}
