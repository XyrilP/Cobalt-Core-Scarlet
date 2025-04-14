using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using XyrilP.ExternalAPI;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XyrilP.Features;

public class FadeManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public FadeManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
        VionheartScarlet.VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.ApplyAutododge)),
            prefix: new HarmonyMethod(GetType(), nameof(AAttack_ApplyAutododge_Prefix))
        );
        VionheartScarlet.VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_OnBeginTurn_Postfix))
        );
    }
    
    public static bool AAttack_ApplyAutododge_Prefix(AAttack __instance, Combat c, Ship target, RaycastResult ray, ref bool __result)
    {
        /////* Uhm... if you have autododge, both fade and autododge will apply... pls fix 👉👈 */
        /* rft50, you were right */
        if (target.Get(VionheartScarlet.VionheartScarlet.Instance.Fade.Status) > 0)
        {
            if (ray.hitShip && !__instance.isBeam)
            {
                /* Decrease Fade once attack is evaded. */
                target.Add(VionheartScarlet.VionheartScarlet.Instance.Fade.Status, -1);
                __result = true;
                return false;
            }
        }
        return true;
    }

    public static void Ship_OnBeginTurn_Postfix(Ship __instance, State s, Combat c)
    {
        if (__instance.Get(Status.timeStop) > 0)
		{
            /* Timestop will decrement itself. */
		}
		else if (__instance.Get(VionheartScarlet.VionheartScarlet.Instance.Fade.Status) > 0)
		{
		    __instance.Add(VionheartScarlet.VionheartScarlet.Instance.Fade.Status, -1);
		}
        
    }
}