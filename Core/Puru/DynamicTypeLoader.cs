using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Nvy;

namespace Puru {
    public class DynamicTypeLoader {
        readonly String loadFromPath;
        readonly IEnumerable<String> namespaces;

        public DynamicTypeLoader(String loadFromPath) : this(loadFromPath, new[] { "*" }) { }

        public DynamicTypeLoader(String loadFromPath, IEnumerable<String> assemblyNames) {
            if (String.IsNullOrEmpty(loadFromPath))
                throw new ArgumentNullException(nameof(loadFromPath));
            if (!Directory.Exists(loadFromPath))
                throw new DirectoryNotFoundException(loadFromPath);
            if (assemblyNames == null)
                throw new ArgumentNullException(nameof(assemblyNames));
            if (!assemblyNames.Any())
                throw new ArgumentOutOfRangeException(nameof(assemblyNames));

            if (assemblyNames.Any(ns => ns.Equals("*")))
                assemblyNames = Directory
                    .EnumerateFiles(loadFromPath, "*.dll", SearchOption.TopDirectoryOnly)
                    .Select(ns => ns.Replace(".dll", String.Empty));

            IList<String> goodNamespaces = assemblyNames
                .Where(ns => !String.IsNullOrEmpty(ns))
                .ToList();

            if (!goodNamespaces.Any())
                throw new ArgumentOutOfRangeException(nameof(assemblyNames));

            IEnumerable<NameValueItem> nonExistents = goodNamespaces
                .Select(ns => Path.Combine(loadFromPath, ns + ".dll"))
                .Select(ns => new NameValueItem(ns, File.Exists(ns).ToString()))
                .Where(asm => !Convert.ToBoolean(asm.Value));

            if (nonExistents.Any()) {
                String message = $"Below assemblies are nowhere to be found. {Environment.NewLine}{String.Join(Environment.NewLine, nonExistents.Select(item => item.Name))}";
                throw new FileNotFoundException(message);
            }

            namespaces = goodNamespaces;
            this.loadFromPath = loadFromPath;
        }

        public DynamicTypeLoader(IEnumerable<String> assemblyNames) : this(AppDomain.CurrentDomain.BaseDirectory, assemblyNames) { }

        public DynamicTypeLoader() : this(AppDomain.CurrentDomain.BaseDirectory) { }

        public IList<Assembly> LoadAssemblies() {
            // https://stackoverflow.com/questions/9315716/side-effects-of-calling-assembly-load-multiple-times
            IList<Assembly> assemblies = namespaces
                .Select(ns => {
                    try {
                        String file = Path.Combine(loadFromPath, ns + ".dll");
                        var asmName = AssemblyName.GetAssemblyName(file);
                        Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == asmName.FullName);
                        if (asm != null)
                            return asm;

                        return Assembly.Load(File.ReadAllBytes(file));
                    }
                    catch {
                        return null;
                    }
                })
                .Where(asm => asm != null)
                .ToList();

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            return assemblies.Any() ? assemblies : null;
        }

        Assembly CurrentDomain_AssemblyResolve(Object sender, ResolveEventArgs args) {
            try {
                if (args.Name.Contains(".resources"))
                    return null;

                Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                if (asm != null)
                    return asm;
            }
            catch {
                return null;
            }

            String[] parts = args.Name.Split(',');
            String file = $"{Path.GetDirectoryName(args.RequestingAssembly.Location)}\\{parts[0].Trim()}.dll";

            return Assembly.Load(File.ReadAllBytes(file));
        }
    }
}