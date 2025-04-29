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

namespace VionheartScarlet.Features;

public class FadeManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public static int fadeDodgeStack = 0;
    public FadeManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)),
            prefix: new HarmonyMethod(GetType(), nameof(AAttack_Begin_Prefix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.ApplyAutododge)),
            prefix: new HarmonyMethod(GetType(), nameof(AAttack_ApplyAutododge_Prefix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_OnBeginTurn_Postfix))
        );
    }
    public static void AAttack_Begin_Prefix(AAttack __instance, G g, State s, Combat c)
    {
        Ship ship = __instance.targetPlayer ? c.otherShip : s.ship;
        if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0 && !__instance.fromDroneX.HasValue)
        {
            __instance.piercing = true;
            ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
        }
    }
    public static bool AAttack_ApplyAutododge_Prefix(AAttack __instance, Combat c, Ship target, RaycastResult ray, ref bool __result)
    {
        if (ray.hitShip && !__instance.isBeam)
        {
            if (target.Get(VionheartScarlet.Instance.Fade.Status) > 0)
            {
                fadeDodgeStack++; // Multiple instances of Fade activation will increase distance travelled.
                /* Decrease Fade once attack is evaded. */
                target.Add(VionheartScarlet.Instance.Fade.Status, -1);

                /* WIP: Instead of cancelling attack, make attack miss. */
                    /* Code here */

                /* Random Move 1 when Fade triggers. */
                c.QueueImmediate(
                    [
                        new AMove()
                        {
                            dir = 0 + fadeDodgeStack,
                            isRandom = true,
                            targetPlayer = !__instance.targetPlayer,
                            timer = 0.0
                        }
                    ]
                );
                __result = true; //This will cause attack to cancel itself.
                return false; //Stop prefixing plus ignore the prefixed method.
            }
        }
        return true; //Continue plus run prefixed method code.
    }
    public static void Ship_OnBeginTurn_Postfix(Ship __instance, State s, Combat c)
    {
        fadeDodgeStack = 0; // Set fadeDodgeStack to 0 at the start of turn.
        if (__instance.Get(Status.timeStop) > 0)
		{
            /* Timestop will decrement itself. */
		}
		else if (__instance.Get(VionheartScarlet.Instance.Fade.Status) > 0)
		{
		    __instance.Add(VionheartScarlet.Instance.Fade.Status, 0);
		}
    }
}