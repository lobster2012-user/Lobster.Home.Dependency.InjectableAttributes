using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes.Reflection
{
    public static class Method<T1>
    {
        private static MethodInfo Of<TBody>(Expression<TBody> expression)
        {
            var memberExpression = expression.Body as MethodCallExpression;
            if (memberExpression == null)
                throw new InvalidOperationException();
            return memberExpression.Method;
        }

        public static MethodInfo Of(Expression<Func<T1>> expression) => Of<Func<T1>>(expression);
        public static MethodInfo Of<T2>(Expression<Func<T1, T2>> expression) => Of<Func<T1, T2>>(expression);
        public static MethodInfo Of<T2, T3>(Expression<Func<T1, T2, T3>> expression) => Of<Func<T1, T2, T3>>(expression);
        public static MethodInfo Of<T2, T3, T4>(Expression<Func<T1, T2, T3, T4>> expression) => Of<Func<T1, T2, T3, T4>>(expression);

        public static MethodInfo Of(Expression<Action<T1>> expression) => Of<Action<T1>>(expression);
        public static MethodInfo Of<T2>(Expression<Action<T1, T2>> expression) => Of<Action<T1, T2>>(expression);
        public static MethodInfo Of<T2, T3>(Expression<Action<T1, T2, T3>> expression) => Of<Action<T1, T2, T3>>(expression);
        public static MethodInfo Of<T2, T3, T4>(Expression<Action<T1, T2, T3, T4>> expression) => Of<Action<T1, T2, T3, T4>>(expression);

    }
}
