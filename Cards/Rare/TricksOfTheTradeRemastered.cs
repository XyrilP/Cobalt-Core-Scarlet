using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using XyrilP.VionheartScarlet.Midrow;

namespace XyrilP.VionheartScarlet.Cards;

public class TricksOfTheTradeRemastered : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "TricksOfTheTrade", "name"]).Localize,
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
                    data.cost = 3;
                    break;
                case Upgrade.B:
                    data.cost = 3;
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
                        statusAmount = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletTricksOfTheTrade"
                    },
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                        }
                    },
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                        },
                        offset = -1
                    },
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                        },
                        offset = 1
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
                        statusAmount = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletTricksOfTheTrade"
                    },
                    new ASpawn()
                    {
                        thing = new YashaDagger_Missile
                        {
                        },
                    },
                    new ASpawn()
                    {
                        thing = new YashaDagger_Missile
                        {
                        },
                        offset = -1
                    },
                    new ASpawn()
                    {
                        thing = new YashaDagger_Missile
                        {
                        },
                        offset = 1
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
                        statusAmount = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletTricksOfTheTrade"
                    },
                    new ASpawn()
                    {
                        thing = new SangeDagger_Missile
                        {
                        },
                    },
                    new ASpawn()
                    {
                        thing = new SangeDagger_Missile
                        {
                        },
                        offset = -1
                    },
                    new ASpawn()
                    {
                        thing = new SangeDagger_Missile
                        {
                        },
                        offset = 1
                    },
                    new AEndTurn()
                };
                break;
        }
        return actions;
    }
}