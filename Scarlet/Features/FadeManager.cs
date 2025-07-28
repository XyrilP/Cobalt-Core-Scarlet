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

namespace VionheartScarlet.Features;

public class FadeManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
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
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AMissileHit), nameof(AMissileHit.Update)),
            prefix: new HarmonyMethod(GetType(), nameof(AMissileHit_Update_Prefix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(IntentGiveCard), nameof(IntentGiveCard.Apply)),
            prefix: new HarmonyMethod(GetType(), nameof(IntentGiveCard_Apply_Prefix))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(IntentStatus), nameof(IntentStatus.Apply)),
            prefix: new HarmonyMethod(GetType(), nameof(IntentStatus_Apply_Prefix))
        );
        /* Prevent player outgoing status */
        // VionheartScarlet.Instance.Harmony.Patch(
        //     original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)),
        //     prefix: new HarmonyMethod(GetType(), nameof(AStatus_Begin_Prefix))
        // );
        /* Prevent player outgoing status */
    }
    public static void AAttack_Begin_Prefix(AAttack __instance, G g, State s, Combat c)
    {
        var aattack = __instance;
        /* Attacks with Fade */
        // Ship ship = __instance.targetPlayer ? c.otherShip : s.ship;
        // if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0 && !__instance.fromDroneX.HasValue)
        // {
        //     __instance.piercing = true; // Make it piercing!
        //     ship.Add(VionheartScarlet.Instance.Fade.Status, -1); // Decrement Fade when you attack!
        // }
        /* Attacks with Fade */
        Ship ship = aattack.targetPlayer ? s.ship : c.otherShip;
        Ship ship2 = aattack.targetPlayer ? c.otherShip : s.ship;
        var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);
        var fadeStatus = VionheartScarlet.Instance.Fade.Status;
        if (fadeValue > 0)
        {
            int? num = aattack.GetFromX(s, c);
            RaycastResult? raycastResult = aattack.fromDroneX.HasValue ? CombatUtils.RaycastGlobal(c, ship, fromDrone: true, aattack.fromDroneX.Value) : num.HasValue ? CombatUtils.RaycastFromShipLocal(s, c, num.Value, aattack.targetPlayer) : null;
            if (raycastResult == null)
            {
                return;
            }
            DamageDone dmg = new DamageDone();
            if (aattack.fromDroneX.HasValue)
            {
                c.stuff.TryGetValue(aattack.fromDroneX.Value, out StuffBase? value);
                if (value is AttackDrone attackDrone)
                {
                    attackDrone.pulse = 1.0;
                }
                if (value is EnergyDrone energyDrone)
                {
                    energyDrone.pulse = 1.0;
                }
                if (value is ShieldDrone shieldDrone)
                {
                    shieldDrone.pulse = 1.0;
                }
                if (value is JupiterDrone jupiterDrone)
                {
                    jupiterDrone.pulse = 1.0;
                }
                if (value is DualDrone dualDrone)
                {
                    dualDrone.pulse = 1.0;
                }
            }
            else
            {
                Part? partAtLocalX = ship2.GetPartAtLocalX(num.HasValue ? num.Value : 0);
                if (partAtLocalX != null)
                {
                    partAtLocalX.pulse = 1.0;
                }
            }
            raycastResult.hitShip = false;
            EffectSpawnerExtension.CannonAngled(g, aattack.targetPlayer, aattack.fromDroneX.HasValue ? aattack.fromDroneX.Value : (num.HasValue ? num.Value : 0), raycastResult, dmg); //Play an animation?
            Audio.Play(StatusMeta.GetSound(fadeStatus, false));
        }
    }
    public static bool AAttack_ApplyAutododge_Prefix(AAttack __instance, Combat c, Ship target, RaycastResult ray, ref bool __result)
    {
        var aattack = __instance;
        if (ray.hitShip && !aattack.isBeam)
        {
            if (target.Get(VionheartScarlet.Instance.Fade.Status) > 0)
            {
                /* Decrease Fade once attack is evaded. */
                target.Add(VionheartScarlet.Instance.Fade.Status, -1);
                /* Decrease Fade once attack is evaded. */
                /* WIP: Instead of cancelling attack, make attack miss. */
                /* Code here */
                /* WIP: Instead of cancelling attack, make attack miss. */
                /* Flag Fade to not decrement if used. */
                VionheartScarlet.Instance.Helper.ModData.SetModData(target, "fadeUsedThisTurn", true);
                /* Flag Fade to not decrement if used. */
                __result = true; //This will cause attack to cancel itself.
                return false; //Stop prefixing plus ignore the prefixed method.
            }
        }
        return true; //Continue plus run prefixed method code.
    }
    public static void Ship_OnBeginTurn_Postfix(Ship __instance, State s, Combat c)
    {
        var ship = __instance;
        var otherShip = c.otherShip;
        bool fadeUsedThisTurn = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(ship, "fadeUsedThisTurn", false);
        bool fadeUsedThisTurnOtherShip = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(otherShip, "fadeUsedThisTurn", false);
        bool hasCloakAndDagger = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(ship, "hasCloakAndDagger", false);
        if (ship.Get(Status.timeStop) > 0)
        {
            /* Timestop will decrement itself. */
        }
        else if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0)
        {
            if (fadeUsedThisTurn || hasCloakAndDagger)
            {
                ship.Set(VionheartScarlet.Instance.Fade.Status, 0);
            }
            else
            {
                ship.Set(VionheartScarlet.Instance.Fade.Status, 0);
            }
        }
        if (otherShip.Get(Status.timeStop) > 0)
        {
            /* Timestop will decrement itself. */
        }
        else if (otherShip.Get(VionheartScarlet.Instance.Fade.Status) > 0)
        {
            if (fadeUsedThisTurnOtherShip)
            {
                otherShip.Set(VionheartScarlet.Instance.Fade.Status, 0);
            }
            else
            {
                otherShip.Set(VionheartScarlet.Instance.Fade.Status, 0);
            }
        }
        VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "fadeUsedThisTurn", false);
        VionheartScarlet.Instance.Helper.ModData.SetModData(otherShip, "fadeUsedThisTurn", false);
    }
    public static void AMissileHit_Update_Prefix(AMissileHit __instance, G g, State s, Combat c)
    {
        var amissilehit = __instance;
        var ship = amissilehit.targetPlayer ? s.ship : c.otherShip;
        var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);
        var fadeStatus = VionheartScarlet.Instance.Fade.Status;
        var shouldMiss = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(amissilehit, "shouldMiss", false);
        if (fadeValue <= 0 && !shouldMiss) return; // This will fix the Kepler but bork the Fade vs. Missiles.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        c.stuff.TryGetValue(amissilehit.worldX, out StuffBase value);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (!(value is Missile missile))
        {
            amissilehit.timer -= g.dt;
            return;
        }
        if (ship == null)
        {
            return;
        }
        RaycastResult raycastResult = CombatUtils.RaycastGlobal(c, ship, fromDrone: true, amissilehit.worldX);
        bool flag = false;
        if (raycastResult.hitShip)
        {
            Part? partAtWorldX = ship.GetPartAtWorldX(raycastResult.worldX);
            if (partAtWorldX == null || partAtWorldX.type != PType.empty)
            {
                if (fadeValue <= 0)
                {
                    flag = true;
                }
            }
        }
        if (Missile.missileData[missile.missileType].seeking)
        {
            raycastResult.worldX = missile.GetSeekerImpact(s, c);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Part partAtWorldX2 = ship.GetPartAtWorldX(raycastResult.worldX);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (fadeValue <= 0)
            {
                flag = partAtWorldX2 != null && partAtWorldX2.type != PType.empty;
            }
        }
        if (!missile.isHitting)
        {
            if ((raycastResult.hitShip || (!raycastResult.hitShip && missile.missileType == MissileType.seeker)) && fadeValue > 0)
            {
                ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
                VionheartScarlet.Instance.Helper.ModData.SetModData(amissilehit, "shouldMiss", true);
                Audio.Play(StatusMeta.GetSound(fadeStatus, false));
            }
            Audio.Play(flag ? Event.Drones_MissileIncoming : Event.Drones_MissileMiss);
            missile.isHitting = true;
        }
        if (!(missile.yAnimation >= 3.5))
        {
            return;
        }
        if (flag && !shouldMiss)
        {
            int num = amissilehit.outgoingDamage;
            foreach (Artifact item in s.EnumerateAllArtifacts())
            {
                num += item.ModifyBaseMissileDamage(s, s.route as Combat, amissilehit.targetPlayer);
            }
            if (num < 0)
            {
                num = 0;
            }
            DamageDone dmg = ship.NormalDamage(s, c, num, raycastResult.worldX);
            EffectSpawner.NonCannonHit(g, amissilehit.targetPlayer, raycastResult, dmg);
            if (amissilehit.xPush != 0)
            {
                c.QueueImmediate(new AMove
                {
                    targetPlayer = amissilehit.targetPlayer,
                    dir = amissilehit.xPush
                }
                );
            }
            Part? partAtWorldX3 = ship.GetPartAtWorldX(raycastResult.worldX);
            if (partAtWorldX3 != null && partAtWorldX3.stunModifier == PStunMod.stunnable)
            {
                c.QueueImmediate(new AStunPart
                {
                    worldX = raycastResult.worldX
                }
                );
            }
            if (amissilehit.status.HasValue && flag)
            {
                c.QueueImmediate(new AStatus
                {
                    status = amissilehit.status.Value,
                    statusAmount = amissilehit.statusAmount,
                    targetPlayer = amissilehit.targetPlayer
                }
                );
            }
            if (amissilehit.weaken && flag)
            {
                c.QueueImmediate(new AWeaken
                {
                    worldX = amissilehit.worldX,
                    targetPlayer = amissilehit.targetPlayer
                }
                );
            }
            if (ship.Get(Status.payback) > 0 || ship.Get(Status.tempPayback) > 0)
            {
                c.QueueImmediate(new AAttack
                {
                    damage = Card.GetActualDamage(s, ship.Get(Status.payback) + ship.Get(Status.tempPayback), !amissilehit.targetPlayer),
                    targetPlayer = !amissilehit.targetPlayer,
                    fast = true
                }
                );
            }
        }
        c.stuff.Remove(amissilehit.worldX);
        if (!(raycastResult.hitDrone || flag) || shouldMiss)
        {
            c.stuffOutro.Add(missile);
        }
    }
    public static bool IntentGiveCard_Apply_Prefix(IntentGiveCard __instance, State s, Combat c, Ship fromShip, int actualX)
    {
        var intentgivecard = __instance;
        var ship = s.ship;
        var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);

        if (fadeValue > 0)
        {
            c.QueueImmediate(
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = -1,
                    targetPlayer = true
                }
            ]
            );
            //ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
            return false;
        }
        return true;
    }
    public static bool IntentStatus_Apply_Prefix(IntentStatus __instance, State s, Combat c, Ship fromShip, int actualX)
    {
        var intentstatus = __instance;
        var ship = s.ship;
        var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);

        if (fadeValue > 0 && !intentstatus.targetSelf)
        {
            c.QueueImmediate(
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = -1,
                    targetPlayer = true
                }
            ]
            );
            //ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
            return false;
        }
        return true;
    }
    /* Prevent player outgoing status */
    // public static bool AStatus_Begin_Prefix(AStatus __instance, G g, State s, Combat c)
    // {
    //     var astatus = __instance;
    //     var ship = c.otherShip;
    //     var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);
    //     var fadeStatus = VionheartScarlet.Instance.Fade.Status;
    //     if (fadeValue > 0 && !astatus.targetPlayer)
    //     {
    //         c.QueueImmediate(
    //         [
    //             new ADummyAction
    //             {
    //                 timer = 0.4
    //             }
    //         ]
    //         );
    //         ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
    //         Audio.Play(StatusMeta.GetSound(fadeStatus, false));
    //         return false;
    //     }
    //     return true;
    // }
    /* Prevent player outgoing status */
}