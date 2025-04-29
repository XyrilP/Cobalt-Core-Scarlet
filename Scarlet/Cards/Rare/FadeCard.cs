using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class FadeCard : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
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
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "FadeCard", "name"]).Localize,
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 3,
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                cost = 2,
                exhaust = true
            },
            Upgrade.B => new CardData
            {
                cost = 3,
                exhaust = true,
                flippable = true
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
                    statusAmount = 3,
                    targetPlayer = true,
                    dialogueSelector = ".scarletFadeAway"
                },
                new AMove
                {
                    dir = 6,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1
                },
                new AEndTurn
                {
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    dialogueSelector = ".scarletFadeAway"
                },
                new AMove
                {
                    dir = 6,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1
                },
                new AEndTurn
                {
                }
            ],
            Upgrade.B =>
            [
                new AStatus()
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    dialogueSelector = ".scarletFadeAway"
                },
                new AMove()
                {
                    dir = 2,
                    targetPlayer = true,
                },
                new AMove()
                {
                    dir = 4,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus()
                {
                    status = Status.lockdown,
                    statusAmount = 1
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