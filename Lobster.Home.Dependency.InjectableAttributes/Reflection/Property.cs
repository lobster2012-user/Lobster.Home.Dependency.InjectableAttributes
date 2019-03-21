using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes.Reflection
{
    public static class Property<T>
    {
        public static MemberInfo Of<TProp>(Expression<Func<T, TProp>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException();
            return memberExpression.Member;
        }
    }
}
