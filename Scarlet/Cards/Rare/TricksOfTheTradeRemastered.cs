using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using Vionheart.Midrow;

namespace Vionheart.Cards;

public class TricksOfTheTradeRemastered : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TricksOfTheTrade.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "TricksOfTheTrade", "name"]).Localize,
            Art = BaseArt?.Sprite
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
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                cost = 2,
                exhaust = true
            },
            Upgrade.B => new CardData
            {
                cost = 2,
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
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    dialogueSelector = ".scarletTricksOfTheTrade"
                },
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                    }
                },
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                    },
                    offset = -2
                },
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                    },
                    offset = 2
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    dialogueSelector = ".scarletTricksOfTheTrade"
                },
                new ASpawn
                {
                    thing = new YashaDagger_Missile
                    {
                    },
                },
                new ASpawn
                {
                    thing = new YashaDagger_Missile
                    {
                    },
                    offset = -2
                },
                new ASpawn
                {
                    thing = new YashaDagger_Missile
                    {
                    },
                    offset = 2
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    dialogueSelector = ".scarletTricksOfTheTrade"
                },
                new ASpawn
                {
                    thing = new SangeDagger_Missile
                    {
                    },
                },
                new ASpawn
                {
                    thing = new SangeDagger_Missile
                    {
                    },
                    offset = -2
                },
                new ASpawn
                {
                    thing = new SangeDagger_Missile
                    {
                    },
                    offset = 2
                }
            ],
            _ => []
        };
    }
}