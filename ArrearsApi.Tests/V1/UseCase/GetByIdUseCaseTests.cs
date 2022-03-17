using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase;
using Hackney.Core.Testing.Shared;
using Moq;
using NUnit.Framework;

namespace ArrearsApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests : LogCallAspectFixture
    {
        private Mock<IBatchLogGateway> _mockGateway;
        private GetBatchLogByIdUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IBatchLogGateway>();
            _classUnderTest = new GetBatchLogByIdUseCase(_mockGateway.Object);
        }

        //TODO: test to check that the use case retrieves the correct record from the database.
        //Guidance on unit testing and example of mocking can be found here https://github.com/LBHackney-IT/lbh-lbh-arrears-api/wiki/Writing-Unit-Tests
    }
}
