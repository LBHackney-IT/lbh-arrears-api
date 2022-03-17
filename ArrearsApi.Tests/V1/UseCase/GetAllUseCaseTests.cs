using System.Linq;
using AutoFixture;
using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase;
using FluentAssertions;
using Hackney.Core.Testing.Shared;
using Moq;
using NUnit.Framework;

namespace ArrearsApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests : LogCallAspectFixture
    {
        private Mock<IBatchLogGateway> _mockGateway;
        private GetAllBatchLogUseCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IBatchLogGateway>();
            _classUnderTest = new GetAllBatchLogUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        //[Test]
        //public void GetsAllFromTheGateway()
        //{
        //    var stubbedEntities = _fixture.CreateMany<BatchLog>().ToList();
        //    _mockGateway.Setup(x => x.GetAll()).Returns(stubbedEntities);

        //    var expectedResponse = new ResponseObjectList { ResponseObjects = stubbedEntities.ToResponse() };

        //    _classUnderTest.Execute().Should().BeEquivalentTo(expectedResponse);
        //}

        //TODO: Add extra tests here for extra functionality added to the use case
    }
}
