using System;
using System.Collections.Generic;
using System.Linq;

namespace Puru {
    public static class TypeExt {
        public static IList<TAttribute> GetDecorators<TAttribute>(this IEnumerable<Type> types) =>
            types?
                .SelectMany(type => type.GetCustomAttributes(false))
                .Cast<TAttribute>()
                .ToList();

        public static Type GetSingleTypeDecoratedBy<TAttribute>(this IEnumerable<Type> types, Func<TAttribute, Boolean> predicate) =>
            types?
                .ToList()
                .SingleOrDefault(type =>
                    predicate(type
                        .GetCustomAttributes(typeof(TAttribute), false)
                        .Cast<TAttribute>()
                        .Single()
                    )
                );
    }
}
