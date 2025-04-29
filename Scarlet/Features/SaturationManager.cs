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

public class SaturationManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public SaturationManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
        VionheartScarlet.Instance.Harmony.Patch(
        original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)),
        postfix: new HarmonyMethod(GetType(), nameof(AStatus_Begin_Postfix))
        );
    }
    public static void AStatus_Begin_Postfix(AStatus __instance, G g, State s, Combat c)
    {
        Ship ship = __instance.targetPlayer ? s.ship : c.otherShip;
        Ship otherShip = __instance.targetPlayer ? c.otherShip : s.ship;
        var statusValue = ship.Get(VionheartScarlet.Instance.Saturation.Status);
        if (ship.Get(VionheartScarlet.Instance.Saturation.Status) >= 2)
        {
            ship.Add(VionheartScarlet.Instance.Saturation.Status, -2);
            // c.QueueImmediate(
            // [
            //     new AHurt
            //     {
            //         targetPlayer = __instance.targetPlayer,
            //         hurtAmount = 1,
            //         hurtShieldsFirst = true
            //     }
            // ]
            // );
            ship.NormalDamage(s, c, 1, null, false);
        }
    }
}