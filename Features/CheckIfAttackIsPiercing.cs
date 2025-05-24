using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Features;

public class CheckIfAttackIsPiercing
{
    public CheckIfAttackIsPiercing(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AAttack_Begin_Postfix))
        );
    }
    public static void AAttack_Begin_Postfix(AAttack __instance, G g, State s, Combat c)
    {
        var aattack = __instance;
        if (aattack.piercing == true)
        {
            VionheartScarlet.Instance.Helper.ModData.SetModData(c, "attackIsPiercing", true);
        }
        else
        {
            VionheartScarlet.Instance.Helper.ModData.SetModData(c, "attackIsPiercing", false);
        }
    }
}