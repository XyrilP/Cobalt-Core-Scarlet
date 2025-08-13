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
using VionheartScarlet.Actions;
using static VionheartScarlet.ExternalAPI.IKokoroApi.IV2.IStatusRenderingApi.IHook;

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
        // VionheartScarlet.Instance.Harmony.Patch(
        //     original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.GetTooltips)),
        //     postfix: new HarmonyMethod(GetType(), nameof(AStatus_GetTooltips_Postfix))
        // );
    }
    public IReadOnlyList<Tooltip> OverrideStatusTooltips(IOverrideStatusTooltipsArgs args)
    {
        var SaturationBarrageStatus = VionheartScarlet.Instance.SaturationBarrage.Status;
        var SaturationStatus = VionheartScarlet.Instance.Saturation.Status;
        if (args.Status != SaturationBarrageStatus) return args.Tooltips;
        else return args.Tooltips.Concat(StatusMeta.GetTooltips(SaturationStatus, 1)).ToList();
    }
    public static void AAttack_Begin_Postfix(AAttack __instance, G g, State s, Combat c)
    {
        var aattack = __instance;
        Ship ship = __instance.targetPlayer ? s.ship : c.otherShip;
        Ship otherShip = __instance.targetPlayer ? c.otherShip : s.ship;
        var statusValue = otherShip.Get(VionheartScarlet.Instance.SaturationBarrage.Status);
        if (otherShip.Get(VionheartScarlet.Instance.SaturationBarrage.Status) > 0)
        {
            for (int i = 0; i < statusValue; i++)
            {
                if (!__instance.targetPlayer && !__instance.fromDroneX.HasValue && g.state.ship.GetPartTypeCount(PType.cannon) > 1 && !__instance.multiCannonVolley)
                {
                }
                else
                    c.QueueImmediate(
                        new ABarrageAttack
                        {
                            targetPlayer = aattack.targetPlayer
                        }
                    );
            }
        }
    }
    public static void AMove_Begin_Postfix(AMove __instance, G g, State s, Combat c)
    {
        var amove = __instance;
        Ship ship = amove.targetPlayer ? s.ship : c.otherShip;
        Ship otherShip = amove.targetPlayer ? c.otherShip : s.ship;
        var statusValue = ship.Get(VionheartScarlet.Instance.SaturationBarrage.Status);
        if (ship.Get(VionheartScarlet.Instance.SaturationBarrage.Status) > 0)
        {
            for (int i = 0; i < statusValue; i++)
            {
                c.QueueImmediate(
                    new ABarrageAttack
                    {
                        targetPlayer = !amove.targetPlayer
                    }
                );
            }
        }
    }
    public static void Ship_OnBeginTurn_Postfix(Ship __instance, State s, Combat c)
    {
        var saturationBarrageStatus = VionheartScarlet.Instance.SaturationBarrage.Status;
        if (__instance.Get(Status.timeStop) > 0)
        {
            /* Timestop will decrement itself. */
        }
        else if (__instance.Get(VionheartScarlet.Instance.SaturationBarrage.Status) > 0)
        {
            __instance.Add(VionheartScarlet.Instance.SaturationBarrage.Status, -1);
            Audio.Play(StatusMeta.GetSound(saturationBarrageStatus, false));
        }
    }
    // public static void AStatus_GetTooltips_Postfix(AStatus __instance, State s, ref List<Tooltip> __result)
    // {
    //     var astatus = __instance;
    //     /* Append Saturation tooltip */
    //     List<Tooltip> saturationTooltip =
    //     [
    //         new GlossaryTooltip($"{VionheartScarlet.Instance.Package.Manifest.UniqueName}::Status::{VionheartScarlet.Instance.Localizations.Localize(["status", "Saturation", "name"])}")
    //         {
    //             Icon = VionheartScarlet.Instance.Helper.Content.Sprites.RegisterSprite(VionheartScarlet.Instance.Package.PackageRoot.GetRelativeFile("assets/icons/Saturation.png")).Sprite,
    //             TitleColor = Colors.status,
    //             Title = VionheartScarlet.Instance.Localizations.Localize(["status", "Saturation", "name"]),
    //             Description = VionheartScarlet.Instance.Localizations.Localize(["status", "Saturation", "description"])
    //         }
    //     ];
    //     __result = saturationTooltip;
    //     /* Append Saturation tooltip */
    // }
}