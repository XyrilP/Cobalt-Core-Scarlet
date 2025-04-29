using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class BarrelRoll : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/BarrelRoll.png")); //Art used.
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "BarrelRoll", "name"]).Localize,
            Art = BaseArt.Sprite
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 1
            },
            Upgrade.A => new CardData
            {
                cost = 1
            },
            Upgrade.B => new CardData
            {
                cost = 1
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
                new AMove()
                {
                    dir = -1,
                    targetPlayer = true
                },
                new AStatus()
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AMove()
                {
                    dir = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.A =>
            [
                new AMove()
                {
                    dir = -1,
                    targetPlayer = true
                },
                new AStatus()
                {
                    status = Status.tempShield,
                    statusAmount = 4,
                    targetPlayer = true
                },
                new AMove()
                {
                    dir = 2,
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
                new AStatus()
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AStatus()
                {
                    status = Status.evade,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}