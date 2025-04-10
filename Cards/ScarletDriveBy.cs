using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletDriveBy : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("DriveBy", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "DriveBy", "name"]).Localize
        
        }
        );

    }

    public override CardData GetData(State state)
    {

        CardData data = new CardData();
        {
            switch (upgrade)
            {
                case Upgrade.A:
                    data.cost = 1;
                    break;
                default:
                    data.cost = 2;
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
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1)
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    }
                };
                break;

        }
        return actions;


    }

}