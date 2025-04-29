using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}