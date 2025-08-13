using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class Afterburn : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/Afterburn.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Afterburn", "name"]).Localize,
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
                exhaust = true,
                retain = true
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                exhaust = true,
                retain = true
            },
            Upgrade.B => new CardData
            {
                cost = 2,
                exhaust = true,
                retain = true
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
                    status = Status.hermes,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.SaturationBarrage.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.loseEvadeNextTurn,
                    targetPlayer = true,
                    statusAmount = 1
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Status.hermes,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.SaturationBarrage.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.loseEvadeNextTurn,
                    targetPlayer = true,
                    statusAmount = 1
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = Status.hermes,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.SaturationBarrage.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.loseEvadeNextTurn,
                    targetPlayer = true,
                    statusAmount = 1
                }
            ],
            _ => []
        };
    }
}