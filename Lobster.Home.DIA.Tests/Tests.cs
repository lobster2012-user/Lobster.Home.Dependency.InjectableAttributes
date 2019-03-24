using Lobster.Home.Dependency.InjectableAttributes;
using Lobster.Home.Dependency.InjectableAttributes.Reflection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Lobster.Home.DIA.Tests
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class Attribute3 : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class Attribute4 : Attribute3
    {
        public string Attr { get; set; }
    }
    [Attribute4(Attr = "new Attribute3()")]
    public class SomeClass3
    {

    }
    public class SomeClass4  : SomeClass3
    {

    }
    public class Tests
    {
        private static readonly string customAttributeNameForClass = nameof(customAttributeNameForClass);
        private static readonly string customAttributeNameForProperty = nameof(customAttributeNameForProperty);

        [Test]
        public void SubsclassTest()
        {
            Assert.True(typeof(SomeClass3).IsAssignableFrom(typeof(SomeClass3)));
        }

        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void DerivedAttributes()
        {
            Assert.AreEqual(1, typeof(SomeClass3).GetCustomAttributes<Attribute4>(inherit: true).Count());
        }
        [Test]
        public void DerivedAttributes2()
        {
            Assert.AreEqual(1, typeof(SomeClass3).GetCustomAttributes<Attribute4>(inherit: true).Count());
        }
        [Test]
        public void DerivedAttributes3()
        {
            Assert.AreEqual(1, typeof(SomeClass4).GetCustomAttributes<Attribute4>(inherit: true).Count());
        }
        [Test]
        public void DerivedAttributes4()
        {
            Assert.AreEqual(1, typeof(SomeClass4).GetCustomAttributes<Attribute4>(inherit: true).Count());
        }
        [Test]
        public void DerivedAttributes7()
        {
            Assert.AreEqual(1, typeof(SomeClass4).GetCustomAttributes<Attribute3>(inherit: true).Count());
        }
        [Test]
        public void DerivedAttributes8()
        {
            Assert.AreEqual(1, typeof(SomeClass4).GetCustomAttributes<Attribute3>(inherit: true).Count());
        }
        [Test]
        public void DerivedAttributes5()
        {
            Assert.AreEqual(0, typeof(SomeClass4).GetCustomAttributes<Attribute4>(inherit: false).Count());
        }
        [Test]
        public void DerivedAttributes6()
        {
            Assert.AreEqual(0, typeof(SomeClass4).GetCustomAttributes<Attribute4>(inherit: false).Count());
        }

        [Test]
        public void DerivedAttributes9()
        {
            Assert.AreEqual(0, typeof(SomeClass4).GetCustomAttributes<Attribute3>(inherit: true).Count());
        }

        /*
        [Test]
        public void DefaultAttributeResolver_GetCustomAttribute_Class()
        {
            var r = new DefaultMetaAttributeResolver();

            var attrOfClass = r.GetCustomAttribute<SomeAttribute>(typeof(SomeClass));
            Assert.NotNull(attrOfClass);
            Assert.AreEqual(nameof(SomeClass), attrOfClass.Name);
        }
        [Test]
        public void DefaultAttributeResolver_GetCustomAttribute_Property()
        {
            var r = new DefaultMetaAttributeResolver();

            var prop = typeof(SomeClass).GetProperty(nameof(SomeClass.SomeProperty));
            Assert.NotNull(prop);

            var attrOfProperty = r.GetCustomAttribute<SomeAttribute>(prop);
            Assert.NotNull(attrOfProperty);
            Assert.AreEqual(nameof(SomeClass.SomeProperty), attrOfProperty.Name);
        }
        [Test]
        public void CustomAttributeResolver_AddAttributes_Class()
        {
            var r = new CustomAttributeResolverThreadUnsafe()
                        .Add<SomeClass2, SomeAttribute>(new SomeAttribute(name: customAttributeNameForClass));

            var attrOfClass = r.GetCustomAttribute<SomeAttribute>(typeof(SomeClass2));
            Assert.NotNull(attrOfClass);
            Assert.AreEqual(customAttributeNameForClass, attrOfClass.Name);
        }
        [Test]
        public void CustomAttributeResolver_AddAttributes_Property()
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
                        new DefaultMetaAttributeResolver(),
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
                       new DefaultMetaAttributeResolver(),
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
                       new DefaultMetaAttributeResolver(),
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


        [Test]
        public void CustomAttributeResolver_PropertyExpression()
        {
            var prop = typeof(SomeClass2).GetProperty(nameof(SomeClass2.SomeProperty2));
            Assert.NotNull(prop);

            var r = new CustomAttributeResolverThreadUnsafe()
                        .Add(Property<SomeClass2>.Of(obj => obj.SomeProperty2), new SomeAttribute(name: customAttributeNameForProperty));

            var attrOfProperty = r.GetCustomAttribute<SomeAttribute>(prop);
            Assert.NotNull(attrOfProperty);
            Assert.AreEqual(customAttributeNameForProperty, attrOfProperty.Name);
        }
        [Test]
        public void CustomAttributeResolver_MethodExpression()
        {
            var prop = typeof(SomeClass2).GetMethod(nameof(SomeClass2.SomeMethod2));
            Assert.NotNull(prop);

            var r = new CustomAttributeResolverThreadUnsafe()
                        .Add(Method<SomeClass2>.Of(obj => obj.SomeMethod2()), new SomeAttribute(name: customAttributeNameForProperty));

            var attrOfProperty = r.GetCustomAttribute<SomeAttribute>(prop);
            Assert.NotNull(attrOfProperty);
            Assert.AreEqual(customAttributeNameForProperty, attrOfProperty.Name);
        }
        */
    }
}