using Microsoft.Extensions.Logging;

namespace SampleStack.AutoMapper.Logging
{
    public static partial class LoggingExtensions
    {
        [LoggerMessage(LogLevel.Debug, "Mapping started from {SourceType} to {DestinationType}")]
        public static partial void DebugMappingStarted(this ILogger logger, Type sourceType, Type destinationType);

        [LoggerMessage(LogLevel.Debug, "Mapping completed from {SourceType} to {DestinationType}")]
        public static partial void DebugMappingCompleted(this ILogger logger, Type sourceType, Type destinationType);

        [LoggerMessage(LogLevel.Error, "Error occurred while mapping from {SourceType} to {DestinationType}.")]
        public static partial void ErrorMapping(this ILogger logger, Exception ex, Type sourceType, Type destinationType);
    }
}