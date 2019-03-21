using Lobster.Home.Dependency.InjectableAttributes.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class CustomAttributeResolverThreadUnsafe : AttributeResolverBase
    {
        private Dictionary<MemberInfo, Dictionary<Type, List<Attribute>>>
            _attributes = new Dictionary<MemberInfo, Dictionary<Type, List<Attribute>>>();

        public bool BreakIfNotNull { get; set; }

        private List<Attribute> Get<TAttribute>(MemberInfo mi)
        {
            return Get(mi, typeof(TAttribute));
        }

        private List<Attribute> Get(MemberInfo mi, Type attribute)
        {
            if (!_attributes.TryGetValue(mi, out var dict)) return null;
            return dict.GetValueOrDefault(attribute);
        }

        private List<Attribute> GetOrAdd<TAttribute>(MemberInfo mi)
        {
            return _attributes.GetOrAdd(mi).GetOrAdd(typeof(TAttribute));
        }

        private void AssertNotNull<T>(T src, string name)
        {
            if (src == null) { throw new ArgumentNullException(name); }
        }

        public override IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi)
        {
            var list = Get<TAttribute>(mi);
            if (list == null)
            {
                return Array.Empty<TAttribute>();
            }
            var typedList = list.Cast<TAttribute>();
            if (BreakIfNotNull)
            {
                return new BreakableEnumerable<TAttribute>(typedList);
            }
            return typedList;
        }

        public CustomAttributeResolverThreadUnsafe Add<TType, TAttribute>(TAttribute attribute)
             where TAttribute : Attribute
        {
            return Add<TAttribute>(typeof(TType), attribute);
        }

        public CustomAttributeResolverThreadUnsafe Add<TType, TProp, TAttribute>(
            Expression<Func<TType, TProp>> expression,
            TAttribute attribute)
             where TAttribute : Attribute
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException();
            return Add<TAttribute>(memberExpression.Member, attribute);
        }
        public CustomAttributeResolverThreadUnsafe Add<TType, TAttribute>(
            Expression<Action<TType>> expression,
            TAttribute attribute)
             where TAttribute : Attribute
        {
            var memberExpression = expression.Body as MethodCallExpression;
            if (memberExpression == null)
                throw new InvalidOperationException();
            return Add<TAttribute>(memberExpression.Method, attribute);
        }

        public CustomAttributeResolverThreadUnsafe Add<TAttribute>(MemberInfo mi, TAttribute attribute)
            where TAttribute : Attribute
        {
            AssertNotNull(mi, nameof(mi));
            AssertNotNull(attribute, nameof(attribute));

            GetOrAdd<TAttribute>(mi).Add(attribute);
            return this;
        }

        public CustomAttributeResolverThreadUnsafe Clear<TAttribute>(MemberInfo mi)
           where TAttribute : Attribute
        {
            return Clear(mi, typeof(TAttribute));
        }

        public CustomAttributeResolverThreadUnsafe Clear(MemberInfo mi, Type attribute)
        {
            AssertNotNull(mi, nameof(mi));
            AssertNotNull(attribute, nameof(attribute));
            var list = Get(mi, attribute);
            if (list != null)
            {
                list.Clear();
            }
            return this;
        }

        public CustomAttributeResolverThreadUnsafe AddOrReplace<TAttribute>(MemberInfo mi, TAttribute attribute)
            where TAttribute : Attribute
        {
            AssertNotNull(mi, nameof(mi));
            AssertNotNull(attribute, nameof(attribute));

            var list = GetOrAdd<TAttribute>(mi);
            list.Clear();
            list.Add(attribute);

            return this;
        }
    }
}
