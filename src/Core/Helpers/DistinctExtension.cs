using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public class GenericComparator<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> MetodoEquals { get; }
        public Func<T, int> MetodoGetHashCode { get; }
        private GenericComparator(
            Func<T, T, bool> metodoEquals,
            Func<T, int> metodoGetHashCode
        )
        {
            this.MetodoEquals = metodoEquals;
            this.MetodoGetHashCode = metodoGetHashCode;
        }

        public static GenericComparator<T> Criar(
            Func<T, T, bool> metodoEquals,
            Func<T, int> metodoGetHashCode)
                => new GenericComparator<T>(
                        metodoEquals,
                        metodoGetHashCode
                    );

        public bool Equals(T x, T y) => MetodoEquals(x, y);

        public int GetHashCode(T obj) => MetodoGetHashCode(obj);
    }

    public static class DistinctExtension
    {
        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, bool> metodoEquals,
            Func<TSource, int> metodoGetHashCode)
                => source.Distinct(
                    GenericComparator<TSource>.Criar(
                        metodoEquals,
                        metodoGetHashCode)
                        );
    }

}
