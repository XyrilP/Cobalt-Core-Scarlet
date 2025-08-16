using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VionheartScarlet.ExternalAPI;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FSPRO;
using System.Runtime.InteropServices;
using System.Collections;

namespace VionheartScarlet.Features;

public class BarrelRollManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public BarrelRollManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(State), nameof(State.Update)),
            postfix: new HarmonyMethod(GetType(), nameof(State_Update_Postfix))
        );
        // VionheartScarlet.Instance.Harmony.Patch(
        //     original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)),
        //     postfix: new HarmonyMethod(GetType(), nameof(AStatus_Begin_Postfix))
        // );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_OnBeginTurn_Postfix))
        );
    }
    public static void State_Update_Postfix(State __instance, G g)
    {
        var s = __instance;
        var ship = s.ship;
        var barrelRollStatus = VionheartScarlet.Instance.BarrelRoll.Status;
        var barrelRollValue = ship.Get(barrelRollStatus);
        var barrelRollInverted = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(ship, "barrelRollInverted", false);
        if (s.route is Combat c)
        {
            var otherShip = c.otherShip;
            var otherBarrelRollValue = otherShip.Get(barrelRollStatus);
            var otherBarrelRollInverted = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(otherShip, "barrelRollInverted", false);
        }
        if (barrelRollValue > 0 && !barrelRollInverted)
        {
            ship.parts.Reverse();
            foreach (Part part in ship.parts)
            {
                part.flip = !part.flip;
            }
            VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "barrelRollInverted", true);
        }
        else if (barrelRollValue <= 0 && barrelRollInverted)
        {
            ship.parts.Reverse();
            foreach (Part part in ship.parts)
            {
                part.flip = !part.flip;
            }
            VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "barrelRollInverted", false);
        }
    }
    public static void AStatus_Begin_Postfix(AStatus __instance, G g, State s, Combat c)
    {
        var astatus = __instance;
        var ship = astatus.targetPlayer ? c.otherShip : s.ship;
        var barrelRollStatus = VionheartScarlet.Instance.BarrelRoll.Status;
        var barrelRollValue = ship.Get(barrelRollStatus);
        if (barrelRollValue > 0) VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "barrelRollInverted", true);
        else VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "barrelRollInverted", false);
        var barrelRollInverted = VionheartScarlet.Instance.Helper.ModData.GetModData<bool>(ship, "barrelRollInverted");
        if (barrelRollInverted)
        {
            ship.parts.Reverse();
            foreach (Part part in ship.parts)
            {
                part.flip = !part.flip;
            }
        }
        else
        {
            ship.parts.Reverse();
            foreach (Part part in ship.parts)
            {
                part.flip = !part.flip;
            }
        }
    }
    public static void Ship_OnBeginTurn_Postfix(Ship __instance, State s, Combat c)
    {
        var ship = __instance;
        var otherShip = c.otherShip;
        var barrelRollStatus = VionheartScarlet.Instance.BarrelRoll.Status;
        if (ship.Get(Status.timeStop) > 0)
        {
        }
        else if (ship.Get(barrelRollStatus) > 0)
        {
            ship.Add(barrelRollStatus, -1);
        }
        if (otherShip.Get(Status.timeStop) > 0)
        {
        }
        else if (otherShip.Get(barrelRollStatus) > 0)
        {
            otherShip.Add(barrelRollStatus, -1);
        }
    }
}