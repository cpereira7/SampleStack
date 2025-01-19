namespace SampleStack.AutoMapper.Mapping.Tests
{
    internal record SourceRecord
    {
        public string? Property1 { get; set; }
        public int Property2 { get; set; }
    }

    internal record DestinationRecord
    {
        public string? Property1 { get; set; }
        public int Property2 { get; set; }
    }
}
