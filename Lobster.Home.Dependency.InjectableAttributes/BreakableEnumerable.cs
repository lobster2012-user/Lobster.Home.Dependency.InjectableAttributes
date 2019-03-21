using System;
using System.Collections;
using System.Collections.Generic;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class BreakableEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _src;

        public BreakableEnumerable(IEnumerable<T> src)
        {
            _src = src ?? throw new ArgumentNullException(nameof(src));
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _src.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
