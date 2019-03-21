
namespace Lobster.Home.DIA.Tests
{
    [Some(nameof(SomeClass))]
    public class SomeClass
    {
        [Some(nameof(SomeProperty))]
        public string SomeProperty { get; set; }
    }
}