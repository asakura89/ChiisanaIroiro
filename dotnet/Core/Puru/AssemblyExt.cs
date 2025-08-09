using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Puru {
    public static class AssemblyExt {
        public static IEnumerable<Type> GetTypesDecoratedBy<TAttribute>(this IEnumerable<Assembly> assemblies) =>
            assemblies?
                .SelectMany(asm => asm.GetTypes())
                .Where(type => type
                    .GetCustomAttributes(typeof(TAttribute), false)
                    .Any());

        public static IList<TAttribute> GetDecorators<TAttribute>(this IEnumerable<Assembly> assemblies) =>
            assemblies
                .GetTypesDecoratedBy<TAttribute>()
                .GetDecorators<TAttribute>();

        public static Type GetSingleTypeDecoratedBy<TAttribute>(this IEnumerable<Assembly> assemblies, Func<TAttribute, Boolean> predicate) =>
            assemblies
                .GetTypesDecoratedBy<TAttribute>()
                .GetSingleTypeDecoratedBy(predicate);

        public static IEnumerable<Type> GetTypesInheritedBy<TAncestor>(this IEnumerable<Assembly> assemblies) =>
            assemblies?
                .SelectMany(asm => asm.GetTypes())
                .Where(type => typeof(TAncestor)
                    .IsAssignableFrom(type));
    }
}
