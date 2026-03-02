using System.Reflection;

namespace Reflx;

public class AssemblyLoader : IAssemblyLoader {
    public void LoadFromPath(String path) =>
        LoadFromPath(path, new[] { "*" });

    public void LoadFromPath(String path, IEnumerable<String> assemblyNames) {
        if (String.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(path));
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException(path);
        if (assemblyNames == null)
            throw new ArgumentNullException(nameof(assemblyNames));
        if (!assemblyNames.Any())
            throw new ArgumentOutOfRangeException(nameof(assemblyNames));

        if (assemblyNames.Any(ns => ns.Equals("*")))
            assemblyNames = Directory
                .GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                .Select(ns => Path.GetFileNameWithoutExtension(ns));

        IList<String> goodNamespaces = assemblyNames
            .Where(ns => !String.IsNullOrEmpty(ns))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (!goodNamespaces.Any())
            throw new ArgumentOutOfRangeException(nameof(assemblyNames));

        IEnumerable<KeyValuePair<String, String>> nonExistents = goodNamespaces
            .Select(ns => Path.Combine(path, ns + ".dll"))
            .Select(ns => new KeyValuePair<String, String>(ns, File.Exists(ns).ToString()))
            .Where(asm => !Convert.ToBoolean(asm.Value));

        if (nonExistents.Any()) {
            String message = $"Below assemblies are nowhere to be found. {Environment.NewLine}{String.Join(Environment.NewLine, nonExistents.Select(item => item.Key))}";
            throw new FileNotFoundException(message);
        }

        IList<String> pendingFiles = goodNamespaces
            .Select(ns => Path.Combine(path, ns + ".dll"))
            .ToList();

        Boolean hasLoadedAssembly;
        do {
            hasLoadedAssembly = false;
            foreach (String file in pendingFiles.ToList()) {
                try {
                    var assemblyName = AssemblyName.GetAssemblyName(file);
                    Assembly assembly = AppDomain
                        .CurrentDomain
                        .GetAssemblies()
                        .FirstOrDefault(a => a.FullName == assemblyName.FullName);

                    if (assembly == null)
                        Assembly.LoadFrom(file);

                    pendingFiles.Remove(file);
                    hasLoadedAssembly = true;
                }
                catch { }
            }
        }
        while (hasLoadedAssembly && pendingFiles.Any());
    }
}
