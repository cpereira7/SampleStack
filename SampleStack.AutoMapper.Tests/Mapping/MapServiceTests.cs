using AutoMapper;
using NSubstitute;
using Microsoft.Extensions.Logging;

namespace SampleStack.AutoMapper.Mapping.Tests
{
    public class MapServiceTests
    {
        private readonly IMapper mapperMock;
        private readonly ILogger<MapService> loggerMock;
        private readonly MapService mapService;

        public MapServiceTests()
        {
            mapperMock = Substitute.For<IMapper>();
            loggerMock = Substitute.For<ILogger<MapService>>();

            mapService = new MapService(mapperMock, loggerMock);
        }

        [Fact()]
        public void Map_ValidMapping_ReturnsMappedObject()
        {
            // Arrange
            var sourceObject = new SourceRecord
            {
                Property1 = "Test",
                Property2 = 1
            };

            var destinationObject = new DestinationRecord
            {
                Property1 = "Test",
                Property2 = 1
            };

            mapperMock.Map<SourceRecord, DestinationRecord>(sourceObject).Returns(destinationObject);
            loggerMock.IsEnabled(LogLevel.Debug).Returns(true);

            // Act
            var result = mapService.Map<SourceRecord, DestinationRecord>(sourceObject);

            // Assert
            Assert.Equal(destinationObject, result);

            loggerMock.Received(2).Log(
                LogLevel.Debug,
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                null,
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact()]
        public void Map_InvalidMapping_ThrowsException()
        {
            // Arrange
            var sourceObject = new SourceRecord();
            var exception = new AutoMapperMappingException("Mapping failed. Something went wrong.");

            mapperMock.When(x => x.Map<SourceRecord, DestinationRecord>(sourceObject)).Throw(exception);
            loggerMock.IsEnabled(LogLevel.Error).Returns(true);

            // Act
            void action() => mapService.Map<SourceRecord, DestinationRecord>(sourceObject);

            // Assert
            Assert.Throws<AutoMapperMappingException>(action);

            loggerMock.Received(1).Log(
                Arg.Is<LogLevel>(l => l == LogLevel.Error),
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Is<Exception>(e => e == exception),
                Arg.Any<Func<object, Exception?, string>>());
        }
    }
}