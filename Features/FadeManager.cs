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

namespace XyrilP.VionheartScarlet.Features;

public class FadeManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public FadeManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.ApplyAutododge)),
            prefix: new HarmonyMethod(GetType(), nameof(AAttack_ApplyAutododge_Prefix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_OnBeginTurn_Postfix))
        );

        VionheartScarlet.Instance.Helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnPlayerAttack), (State state, Combat combat) =>
        {
            var ship = state.ship;

            if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0)
            {
                combat.QueueImmediate(
                    [
                        new AStatus()
                        {
                            status = VionheartScarlet.Instance.Fade.Status,
                            statusAmount = -1,
                            targetPlayer = true
                        }
                    ]
                );
            }
        }
        );

        VionheartScarlet.Instance.Helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnEnemyAttack), (State state, Combat combat) =>
        {
            var ship = combat.otherShip;

            if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0)
            {
                combat.QueueImmediate(
                    [
                        new AStatus()
                        {
                            status = VionheartScarlet.Instance.Fade.Status,
                            statusAmount = -1,
                            targetPlayer = false
                        }
                    ]
                );
            }
        }
        );
    }
    public static bool AAttack_ApplyAutododge_Prefix(AAttack __instance, Combat c, Ship target, RaycastResult ray, ref bool __result)
    {
        /////* Uhm... if you have autododge, both fade and autododge will apply... pls fix 👉👈 */
        /* rft50, you were right */

        if (ray.hitShip && !__instance.isBeam)
        {
            if (target.Get(VionheartScarlet.Instance.Fade.Status) > 0)
            {
                /* Decrease Fade once attack is evaded. */
                target.Add(VionheartScarlet.Instance.Fade.Status, -1);

                /* WIP: Instead of cancelling attack, make attack miss. */
                    /* Code here */

                /* Random Move 1 when Fade triggers. */
                c.QueueImmediate(
                    [
                        new AMove()
                        {
                            dir = 1,
                            isRandom = true,
                            targetPlayer = !__instance.targetPlayer,
                            timer = 0.0,
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