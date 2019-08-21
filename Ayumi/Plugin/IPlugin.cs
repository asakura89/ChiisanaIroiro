using System;

namespace Ayumi.Plugin {
    public interface IPlugin {
        String Name { get; }
        String Desc { get; }
        Object Process(Object processArgs);
    }
}
