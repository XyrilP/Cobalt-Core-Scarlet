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

public static class AAttackExtension
{
    public static AAttack ScarletBarrage(this AAttack data)
    {
        VionheartScarlet.Instance.Helper.ModData.SetModData(data, "isScarletBarrageAttack", true);
        return data;
    }
}
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
        bool flag = VionheartScarlet.Instance.Helper.ModData.TryGetModData<bool>(__instance,"isScarletBarrageAttack", out _);
        if (otherShip.Get(VionheartScarlet.Instance.scarletBarrage.Status) > 0)
        {
            if (!flag)
            {
                for(int i = 0; i < statusValue; i++)
                {
                    var offset = s.rngActions.Next();
                    if (offset <= 0.33) offset = 0;
                    else if (offset > 0.33 && offset < 0.66) offset = 1;
                    else offset = 2;
                    c.QueueImmediate(                
                        new AAttack
                        {
                            damage = 0,
                            targetPlayer = __instance.targetPlayer,
                            fast = true,
                            timer = 0.0,
                            fromX = Convert.ToInt32(offset) + ship.parts.FindIndex((Part p) => p.type == PType.cannon && p.active),
                            status = VionheartScarlet.Instance.Saturation.Status,
                            statusAmount = 1
                        }.ScarletBarrage()
                    );
                }
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
            /* Older temp strafe */
            // c.QueueImmediate(
            // new AAttack
            // {
            //     damage = Card.GetActualDamage(s, ship.Get(VionheartScarlet.Instance.TemporaryStrafe.Status)),
            //     targetPlayer = !__instance.targetPlayer,
            //     fast = true,
            //     storyFromStrafe = true
            // }
            // )
            /* Old temp strafe */
            // for(int i = 0; i < temporaryStrafeValue; i++)
            // {
            //     c.QueueImmediate(                
            //         new AAttack
            //         {
            //             damage = Card.GetActualDamage(s, 1),
            //             targetPlayer = !__instance.targetPlayer,
            //             fast = true,
            //             storyFromStrafe = true,
            //             timer = 0.0
            //         }
            //     );
            // }
            for(int i = 0; i < statusValue; i++)
            {
                var offset = s.rngActions.Next();
                if (offset <= 0.33) offset = -1;
                else if (offset > 0.33 && offset < 0.66) offset = 0;
                else offset = 1;
                c.QueueImmediate(                
                    new AAttack
                    {
                        damage = 0, //Card.GetActualDamage(s, 1),
                        targetPlayer = !__instance.targetPlayer,
                        fast = true,
                        storyFromStrafe = true,
                        timer = 0.0,
                        fromX = Convert.ToInt32(offset) + ship.parts.FindIndex((Part p) => p.type == PType.cannon && p.active),
                        status = VionheartScarlet.Instance.Saturation.Status,
                        statusAmount = 1
                    }.ScarletBarrage()
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
		    __instance.Set(VionheartScarlet.Instance.scarletBarrage.Status, 0);
		}
    }
}