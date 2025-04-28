using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class AdjustThrottle : Card, IRegisterable
{
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/AdjustThrottle_Up.png"));
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/AdjustThrottle_Down.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "AdjustThrottle", "name"]).Localize,
            Art = null
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
                cost = 1,
                floppable = true,
                retain = true,
            },
            Upgrade.A => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 0,
                floppable = true,
                retain = true
            },
            Upgrade.B => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 1,
                floppable = true,
                retain = true,
                infinite = true
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
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = Status.engineStall,
                    statusAmount = 2,
                    targetPlayer = true,
                    disabled = !flipped
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 2,
                    targetPlayer = true,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = Status.engineStall,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = Status.engineStall,
                    statusAmount = 2,
                    targetPlayer = true,
                    disabled = !flipped
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            _ => []
        };
    }
}