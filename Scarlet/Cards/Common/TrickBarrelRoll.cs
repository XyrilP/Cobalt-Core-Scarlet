using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Cards;

public class TrickBarrelRoll : Card, IRegisterable
{
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickBarrelRoll_Up.png"));
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickBarrelRoll_Down.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "TrickBarrelRoll", "name"]).Localize,
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 0,
                temporary = true,
                singleUse = true,
                floppable = true
            },
            Upgrade.A => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 0,
                temporary = true,
                singleUse = true,
                floppable = true
            },
            Upgrade.B => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 0,
                temporary = true,
                singleUse = true,
                floppable = true
            },
            _ => new CardData{}
        };
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    disabled = flipped
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 1,
                    mode = AStatusMode.Set,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    disabled = !flipped
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    disabled = !flipped
                },
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    disabled = flipped
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 2,
                    mode = AStatusMode.Set,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    disabled = !flipped
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    disabled = !flipped
                },
            ],
            Upgrade.B =>
            [
                new AInstantTrick
                {
                    amount = 1,
                    disabled = flipped
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 1,
                    mode = AStatusMode.Set,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    disabled = !flipped
                },
                new AInstantTrick
                {
                    amount = 1,
                    disabled = !flipped
                }
            ],
            _ => []
        };
    }
}