using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class Serpentine : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Serpentine", "name"]).Localize,
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
                    break;
                case Upgrade.A:
                    break;
                case Upgrade.B:
                    break;
            }
            data.cost = 2;
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
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 3,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 4,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 3,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 4,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 3,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 4,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 5,
                        targetPlayer = true,
                        isRandom = true
                    }
                };
                break;
        }
        return actions;
    }
}