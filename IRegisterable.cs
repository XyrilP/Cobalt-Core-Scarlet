using Nanoray.PluginManager;
using Nickel;

namespace Vionheart;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}