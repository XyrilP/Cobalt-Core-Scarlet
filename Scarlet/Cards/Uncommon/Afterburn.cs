using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class Afterburn : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "Afterburn", "name"]).Localize,
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
                cost = 3,
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
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Vionheart.Instance.scarletBarrage.Status,
                    statusAmount = 2,
                    targetPlayer = true
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
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Vionheart.Instance.scarletBarrage.Status,
                    statusAmount = 2,
                    targetPlayer = true
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
                    statusAmount = 3,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Vionheart.Instance.scarletBarrage.Status,
                    statusAmount = 2,
                    targetPlayer = true
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