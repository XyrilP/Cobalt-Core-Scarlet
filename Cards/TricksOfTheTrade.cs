using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class TricksOfTheTrade : Card, IRegisterable
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
            data.cost = 2;
            switch (upgrade)
            {
                /* Will localize at some point... pls help */
                case Upgrade.None:
                    data.description = "Attack for 1 dmg, strafing side-to-side 2 times, then return.";
                    break;
                case Upgrade.A:
                    data.description = "Attack for 2 dmg, strafing side-to-side 2 times, then return.";
                    break;
                case Upgrade.B:
                    data.description = "Attack for 1 dmg, strafing side-to-side 4 times, then return.";
                    break;
            }
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
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 3,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = -4,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true
                    }
                };
                break;

        }
        return actions;


    }

}