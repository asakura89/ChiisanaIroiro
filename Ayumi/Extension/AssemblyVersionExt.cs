using System;
using System.Linq;
using System.Reflection;

namespace Ayumi.Extension {
    public static class AssemblyVersionExt {
        static AssemblyVersionAttribute GetAsmVersion(this Assembly asm) => asm
            .GetCustomAttributes(typeof(AssemblyVersionAttribute), false)
            .Cast<AssemblyVersionAttribute>()
            .SingleOrDefault();

        static AssemblyFileVersionAttribute GetAsmFileVersion(this Assembly asm) => asm
            .GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
            .Cast<AssemblyFileVersionAttribute>()
            .SingleOrDefault();

        public static String GetAssemblyVersion(this Assembly asm) {
            AssemblyVersionAttribute asmVersion = asm.GetAsmVersion();
            if (asmVersion != null)
                return asmVersion.Version;

            AssemblyFileVersionAttribute asmFileVersion = asm.GetAsmFileVersion();
            if (asmFileVersion != null)
                return asmFileVersion.Version;

            return "1.0.0.0";
        }
    }
}
