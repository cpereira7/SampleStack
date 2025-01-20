namespace SampleStack.AutoMapper.Mapping
{
    internal interface IMapService
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
