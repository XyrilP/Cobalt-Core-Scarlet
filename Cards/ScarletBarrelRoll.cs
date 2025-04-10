using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletBarrelRoll : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("BarrelRoll", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "BarrelRoll", "name"]).Localize
        
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
                    data.cost = 2;
                    break;
                case Upgrade.A:
                    data.cost = 1;
                    data.flippable = true;
                    break;
                case Upgrade.B:
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
                    new AMove()
                    {
                        dir = 1,
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
                        dir = -2,
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
                        dir = -2,
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
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 4,
                        targetPlayer = true
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