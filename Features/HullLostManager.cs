using Nanoray.PluginManager;
using Nickel;

/* You can put this back, remember to rename stuff accordingly.
namespace DemoMod.Features;
*/
namespace VionheartScarlet.Features;
public class HullLostManager
{
    /* Removed code
        public virtual void OnCombatStart(State state, Combat combat)
        {
            hullLostNumber = 0;
            lastCheckedHullAmount = state.ship.hull;
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "hullLostNumber", hullLostNumber);
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "lastCheckedHullAmount", lastCheckedHullAmount);
        }
        
        public virtual void OnTurnStart(State state, Combat combat)
        {
            hullLostNumber = 0;
            lastCheckedHullAmount = state.ship.hull;
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "hullLostNumber", hullLostNumber);
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "lastCheckedHullAmount", lastCheckedHullAmount);
        }
        
        public virtual void OnPlayerLoseHull(State state, Combat combat)
        {
            detectDamage = lastCheckedHullAmount - state.ship.hull;
            hullLostNumber += detectDamage;
            lastCheckedHullAmount = state.ship.hull;
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "hullLostNumber", hullLostNumber);
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "lastCheckedHullAmount", lastCheckedHullAmount);
        }
    */
    T GetModDataOrDefault<T>(object o, string key, T defaultValue)
    {
        return default!;
    }
    /* Added code */
    int hullLostNumber { get; set; }
    int lastCheckedHullAmount { get; set; }
    int detectDamage { get; set; }
    public HullLostManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.Helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnCombatStart), (State state, Combat combat) =>
        {
            /*
                Is this really necessary though?
            */
            hullLostNumber = 0;
            lastCheckedHullAmount = state.ship.hull;
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "hullLostNumber", hullLostNumber);
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "lastCheckedHullAmount", lastCheckedHullAmount);
        }
        );
        VionheartScarlet.Instance.Helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnTurnStart), (State state, Combat combat) =>
        {
            hullLostNumber = 0;
            lastCheckedHullAmount = state.ship.hull;
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "hullLostNumber", hullLostNumber);
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "lastCheckedHullAmount", lastCheckedHullAmount);
        }
        );
        VionheartScarlet.Instance.Helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnPlayerLoseHull), (State state, Combat combat) =>
        {
            detectDamage = lastCheckedHullAmount - state.ship.hull;
            hullLostNumber += detectDamage;
            lastCheckedHullAmount = state.ship.hull;
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "hullLostNumber", hullLostNumber);
            VionheartScarlet.Instance.Helper.ModData.SetModData(state, "lastCheckedHullAmount", lastCheckedHullAmount);
        }
        );
    }
    /* Added code */
    /* Add the following to your ModEntry
        _ = new HullLostManager(package, helper);
    */
    /* To get hullLostNumber
        var hullLostValue = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(state, "hullLostNumber", 0); //Defaults to 0 if none found to avoid errors.
    */
}