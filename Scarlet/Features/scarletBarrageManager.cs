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
using VionheartScarlet.Actions;

namespace VionheartScarlet.Features;
public class scarletBarrageManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public scarletBarrageManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AAttack_Begin_Postfix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AMove), nameof(AMove.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AMove_Begin_Postfix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_OnBeginTurn_Postfix))
        );
    }
    public static void AAttack_Begin_Postfix(AAttack __instance, G g, State s, Combat c)
    {
        Ship ship = __instance.targetPlayer ? s.ship : c.otherShip;
        Ship otherShip = __instance.targetPlayer ? c.otherShip : s.ship;
        var statusValue = otherShip.Get(VionheartScarlet.Instance.scarletBarrage.Status);
        if (otherShip.Get(VionheartScarlet.Instance.scarletBarrage.Status) > 0)
        {
            for(int i = 0; i < statusValue; i++)
            {
                if (!__instance.targetPlayer && !__instance.fromDroneX.HasValue && g.state.ship.GetPartTypeCount(PType.cannon) > 1 && !__instance.multiCannonVolley)
                {
                }
                else
                c.QueueImmediate(
                    new ABarrageAttack
                    {
                    }
                );
            }
        }
    }
    public static void AMove_Begin_Postfix(AMove __instance, G g, State s, Combat c)
    {
        Ship ship = __instance.targetPlayer ? s.ship : c.otherShip;
        Ship otherShip = __instance.targetPlayer ? c.otherShip : s.ship;
        var statusValue = ship.Get(VionheartScarlet.Instance.scarletBarrage.Status);
        if (ship.Get(VionheartScarlet.Instance.scarletBarrage.Status) > 0)
        {
            for(int i = 0; i < statusValue; i++)
            {
                c.QueueImmediate(                
                    new ABarrageAttack
                    {
                    }
                );
            }
        }
    }
    public static void Ship_OnBeginTurn_Postfix(Ship __instance, State s, Combat c)
    {
        if (__instance.Get(Status.timeStop) > 0)
		{
            /* Timestop will decrement itself. */
		}
		else if (__instance.Get(VionheartScarlet.Instance.scarletBarrage.Status) > 0)
		{
		    __instance.Add(VionheartScarlet.Instance.scarletBarrage.Status, -1);
		}
    }
}