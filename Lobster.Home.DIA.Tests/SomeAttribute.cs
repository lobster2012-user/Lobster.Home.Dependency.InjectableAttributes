using System;

namespace Lobster.Home.DIA.Tests
{
    public class SomeAttribute : Attribute
    {
        public SomeAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}