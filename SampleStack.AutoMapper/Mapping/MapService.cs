using AutoMapper;
using Microsoft.Extensions.Logging;
using SampleStack.AutoMapper.Logging;

namespace SampleStack.AutoMapper.Mapping
{
    public class MapService : IMapService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MapService> _logger;

        public MapService(IMapper mapper, ILogger<MapService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            try
            {
                _logger.DebugMappingStarted(typeof(TSource), typeof(TDestination));

                return _mapper.Map<TSource, TDestination>(source);
            }
            catch (AutoMapperMappingException ex)
            {
                var lastInnerException = GetLastInnerException(ex);

                _logger.ErrorMapping(lastInnerException, typeof(TSource), typeof(TDestination));
                throw;
            }
            catch (Exception ex)
            {
                _logger.ErrorMapping(ex, typeof(TSource), typeof(TDestination));
                throw;
            }
            finally
            {
                _logger.DebugMappingCompleted(typeof(TSource), typeof(TDestination));
            }
        }

        private static Exception GetLastInnerException(Exception exception)
        {
            var lastInnerException = exception;

            while (lastInnerException.InnerException != null)
            {
                lastInnerException = lastInnerException.InnerException;
            }

            return lastInnerException;
        }
    }
}
