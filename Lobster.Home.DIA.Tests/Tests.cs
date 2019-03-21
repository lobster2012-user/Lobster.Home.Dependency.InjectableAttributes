using Lobster.Home.Dependency.InjectableAttributes;
using NUnit.Framework;
using System.Linq;

namespace Lobster.Home.DIA.Tests
{
    public class Tests
    {
        private static readonly string customAttributeNameForClass = nameof(customAttributeNameForClass);
        private static readonly string customAttributeNameForProperty = nameof(customAttributeNameForProperty);

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CustomAttributeResolver_GetCustomAttribute_Class()
        {
            var r = new DefaultAttributeResolver();

            var attrOfClass = r.GetCustomAttribute<SomeAttribute>(typeof(SomeClass));
            Assert.NotNull(attrOfClass);
            Assert.AreEqual(nameof(SomeClass), attrOfClass.Name);
        }
        [Test]
        public void CustomAttributeResolver_GetCustomAttribute_Property()
        {
            var r = new DefaultAttributeResolver();

            var prop = typeof(SomeClass).GetProperty(nameof(SomeClass.SomeProperty));
            Assert.NotNull(prop);

            var attrOfProperty = r.GetCustomAttribute<SomeAttribute>(prop);
            Assert.NotNull(attrOfProperty);
            Assert.AreEqual(nameof(SomeClass.SomeProperty), attrOfProperty.Name);
        }
        [Test]
        public void DefaultAttributeResolver_AddAttributes_Class()
        {
            var r = new CustomAttributeResolverThreadUnsafe()
                        .Add<SomeClass2, SomeAttribute>(new SomeAttribute(name: customAttributeNameForClass));

            var attrOfClass = r.GetCustomAttribute<SomeAttribute>(typeof(SomeClass2));
            Assert.NotNull(attrOfClass);
            Assert.AreEqual(customAttributeNameForClass, attrOfClass.Name);
        }
        [Test]
        public void DefaultAttributeResolver_AddAttributes_Property()
        {
            var prop = typeof(SomeClass2).GetProperty(nameof(SomeClass2.SomeProperty2));
            Assert.NotNull(prop);

            var r = new CustomAttributeResolverThreadUnsafe()
                        .Add(prop, new SomeAttribute(name: customAttributeNameForProperty));

            var attrOfProperty = r.GetCustomAttribute<SomeAttribute>(prop);
            Assert.NotNull(attrOfProperty);
            Assert.AreEqual(customAttributeNameForProperty, attrOfProperty.Name);
        }

        [Test]
        public void CustomAttributeResolver_OverrideAttributes_Property()
        {
            var prop = typeof(SomeClass2).GetProperty(nameof(SomeClass2.SomeProperty2));
            Assert.NotNull(prop);

            var r = new AggregatedAttributeResolver(
                        new DefaultAttributeResolver(),
                        new CustomAttributeResolverThreadUnsafe()
                            .Add(prop, new SomeAttribute(name: customAttributeNameForProperty)));

            var attrOfProperty = r.GetCustomAttribute<SomeAttribute>(prop);
            Assert.NotNull(attrOfProperty);
            Assert.AreEqual(customAttributeNameForProperty, attrOfProperty.Name);
        }

        [Test]
        public void CustomAttributeResolver_OverrideAttributes_Class()
        {
            var r = new AggregatedAttributeResolver(
                       new DefaultAttributeResolver(),
                       new CustomAttributeResolverThreadUnsafe()
                           .Add<SomeClass, SomeAttribute>(new SomeAttribute(name: customAttributeNameForClass)));

            var attrOfClass = r.GetCustomAttribute<SomeAttribute>(typeof(SomeClass));
            Assert.NotNull(attrOfClass);
            Assert.AreEqual(customAttributeNameForClass, attrOfClass.Name);
        }

        [Test]
        public void CustomAttributeResolver_Break_Class()
        {
            var custom = new CustomAttributeResolverThreadUnsafe()
                           .Add<SomeClass, SomeAttribute>(new SomeAttribute(name: customAttributeNameForClass));
            var r = new AggregatedAttributeResolver(
                       new DefaultAttributeResolver(),
                           custom);
            {
                var attrOfClass = r.GetCustomAttributes<SomeAttribute>(typeof(SomeClass)).ToArray();
                Assert.AreEqual(2, attrOfClass.Length);
            }
            {
                custom.BreakIfNotNull = true;
                var attrOfClass = r.GetCustomAttributes<SomeAttribute>(typeof(SomeClass)).ToArray();
                Assert.AreEqual(1, attrOfClass.Length);
                Assert.AreEqual(customAttributeNameForClass, attrOfClass[0].Name);
            }
            {
                custom.BreakIfNotNull = false;
                var attrOfClass = r.GetCustomAttributes<SomeAttribute>(typeof(SomeClass)).ToArray();
                Assert.AreEqual(2, attrOfClass.Length);
            }
        }
    }
}