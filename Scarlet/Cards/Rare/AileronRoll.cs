using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class AileronRoll : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/AileronRoll.png")); //Art used.
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "AileronRoll", "name"]).Localize,
            Art = BaseArt?.Sprite //Card art
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 2,
                retain = true,
                infinite = true
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                retain = true,
                infinite = true
            },
            Upgrade.B => new CardData
            {
                cost = 3,
                retain = true,
                exhaust = true
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
                    isRandom = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 1,
                    mode = AStatusMode.Set
                },
                new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                },
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 1,
                    mode = AStatusMode.Set
                },
                new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.B =>
            [
                new AMove()
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.BarrelRoll.Status,
                    targetPlayer = true,
                    statusAmount = 1,
                    mode = AStatusMode.Set
                },
                new AStatus()
                {
                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}