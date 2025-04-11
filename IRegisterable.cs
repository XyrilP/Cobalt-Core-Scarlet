using Nanoray.PluginManager;
using Nickel;

namespace XyrilP.VionheartScarlet;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}