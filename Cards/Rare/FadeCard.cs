using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class FadeCard : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "FadeCard", "name"]).Localize,
        }
        );
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        {
            switch (upgrade)
            {
                case Upgrade.None:
                    data.cost = 3;
                    break;
                case Upgrade.A:
                    data.cost = 2;
                    break;
                case Upgrade.B:
                    data.cost = 3;
                    data.flippable = true;
                    break;
            }
            data.exhaust = true;
        }
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 3,
                        targetPlayer = true,
                        dialogueSelector = ".scarletFadeAway"
                    },
                    new AMove()
                    {
                        dir = 6,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1
                    },
                    new AEndTurn()
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 3,
                        targetPlayer = true,
                        dialogueSelector = ".scarletFadeAway"
                    },
                    new AMove()
                    {
                        dir = 6,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1
                    },
                    new AEndTurn()
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
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
                };
                break;
        }
        return actions;
    }
}