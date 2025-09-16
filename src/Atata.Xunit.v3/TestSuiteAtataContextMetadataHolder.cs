namespace Atata.Xunit;

internal static class TestSuiteAtataContextMetadataHolder
{
    internal static ConcurrentDictionary<Type, TestSuiteAtataContextMetadata> Items { get; } = [];
}
