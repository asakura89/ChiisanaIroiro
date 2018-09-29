using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Ayumi {
    public class DynamicTypeLoader {
        readonly Boolean isWebApp;
        readonly IEnumerable<String> namespaces;

        public DynamicTypeLoader(IEnumerable<String> namespaces, Boolean isWebApp = false) {
            if (namespaces == null)
                throw new ArgumentNullException(nameof(namespaces));
            if (!namespaces.Any())
                throw new ArgumentOutOfRangeException(nameof(namespaces));

            IList<String> goodNamespaces = namespaces
                .Where(ns => !String.IsNullOrEmpty(ns))
                .ToList();

            if (!goodNamespaces.Any())
                throw new ArgumentOutOfRangeException(nameof(namespaces));

            this.namespaces = goodNamespaces;
            this.isWebApp = isWebApp;
        }

        IList<Assembly> LoadAssemblies() {
            String baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // NOTE: Thank God it'll load once 
            // https://stackoverflow.com/questions/9315716/side-effects-of-calling-assembly-load-multiple-times
            IList<Assembly> assemblies = namespaces
                .Select(@namespace => {
                    try {
                        return Assembly.Load(
                            File.ReadAllBytes(
                                Path.Combine(isWebApp ?
                                    Path.Combine(baseDir, "bin") :
                                    baseDir, @namespace + ".dll")));
                    }
                    catch {
                        return null;
                    }
                })
                .Where(asm => asm != null)
                .ToList();

            if (!assemblies.Any())
                return null;

            return assemblies;
        }

        public static IEnumerable<Type> GetTypesDecoratedBy<TAttribute>(IEnumerable<Assembly> assemblies) {
            if (assemblies == null)
                return null;

            return assemblies
                .SelectMany(asm => asm.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(TAttribute), false).Any());

            /*

            // I don't understand why this can't work for AttendanceDataFormatAttribute but flawless for ExcelReportCollectionAttribute
            return assemblies
                .SelectMany(asm => asm.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(TAttribute), false).Any());

            // Okay, change to another way
            IEnumerable<Type> allTypes = assemblies.SelectMany(asm => asm.GetTypes());
            IEnumerable<Type> filteredTypes = allTypes
                .Where(type => type
                    .GetCustomAttributes(false)
                    .Cast<Attribute>()
                    .Select(attr => ((Type) attr.TypeId).FullName.ToLowerInvariant())
                    .Contains(typeof(TAttribute).FullName.ToLowerInvariant()));

            return filteredTypes;

            // It works for ExcelReportCollectionAttribute but still failed for AttendanceDataFormatAttribute
            // It produce error like so
            Error! : [A]Enta.Module.Attendance.Standard.AttendanceDataFormatAttribute cannot be cast to
            [B]Enta.Module.Attendance.Standard.AttendanceDataFormatAttribute. Type A originates from 'Enta.Module.Attendance.Standard,
            Version=5.0.2017.426, Culture=neutral, PublicKeyToken=null' in the context 'LoadNeither' in a byte array. Type B originates from
            'Enta.Module.Attendance.Standard, Version=5.0.2017.426, Culture=neutral, PublicKeyToken=null' in the context 'Default'
            at location 'C:\Users\dita\AppData\Local\Temp\Temporary ASP.NET Files\vs\84f83024\14a9db08\assembly\dl3\53fdd7c2\76bcd262_495ad301\Enta.Module.Attendance.Standard.dll'. 

            // I think I understand the problem. It basically said, I first load AttendanceDataFormatAttribute from Enta.Module.Attendance.Standard namespace.
            // It loads into Default context. Then I dynamically load the Enta.Module.Attendance.Standard dll, and it loads into LoadNeither context.
            // Because it both has AttendanceDataFormatAttribute, and I refer to AttendanceDataFormatAttribute in Default context, it will not match
            // the AttendanceDataFormatAttribute on LoadNeither context. That's why! It explains why this code `type.GetCustomAttributes(typeof(TAttribute)` will always fail.
            // Because it both loads in different context. So, they have to be in the different assemblies.
            // Thanks to `.Cast<TAttribute>()` from GetDecorators. ＼(＾▽＾)／

            */
        }

        public IEnumerable<Type> GetTypesDecoratedBy<TAttribute>() =>
            GetTypesDecoratedBy<TAttribute>(LoadAssemblies());

        public static IList<TAttribute> GetDecorators<TAttribute>(IEnumerable<Assembly> assemblies) =>
            GetDecorators<TAttribute>(GetTypesDecoratedBy<TAttribute>(assemblies));

        public IList<TAttribute> GetDecorators<TAttribute>() =>
            GetDecorators<TAttribute>(GetTypesDecoratedBy<TAttribute>());

        public static IList<TAttribute> GetDecorators<TAttribute>(IEnumerable<Type> types) {
            if (types == null)
                return null;

            return types
                .SelectMany(type => type.GetCustomAttributes(false))
                .Cast<TAttribute>()
                .ToList();
        }

        public static Type GetSingleTypeDecoratedBy<TAttribute>(IEnumerable<Assembly> assemblies, Func<TAttribute, Boolean> predicate) =>
            GetSingleTypeDecoratedBy(GetTypesDecoratedBy<TAttribute>(assemblies), predicate);

        public Type GetSingleTypeDecoratedBy<TAttribute>(Func<TAttribute, Boolean> predicate) =>
            GetSingleTypeDecoratedBy<TAttribute>(LoadAssemblies(), predicate);

        public static Type GetSingleTypeDecoratedBy<TAttribute>(IEnumerable<Type> types,
            Func<TAttribute, Boolean> predicate) {
            if (types == null)
                return null;

            return types
                .ToList()
                .SingleOrDefault(type => predicate(type
                    .GetCustomAttributes(typeof(TAttribute), false)
                    .Cast<TAttribute>().Single()));
        }

        public static IEnumerable<Type> GetTypesInheritedBy<TAncestor>(IEnumerable<Assembly> assemblies) {
            if (assemblies == null)
                return null;

            return assemblies
                .SelectMany(asm => asm.GetTypes())
                .Where(type => typeof(TAncestor).IsAssignableFrom(type));
        }

        public IEnumerable<Type> GetTypesInheritedBy<TAncestor>() =>
            GetTypesInheritedBy<TAncestor>(LoadAssemblies());
    }
}