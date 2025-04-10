using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletFadeCard : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("FadeCard", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "FadeCard", "name"]).Localize
        
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
                    new AStunShip(),
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
                    new AEndTurn()
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStunShip(),
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
                    new AEndTurn()
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStunShip(),
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
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                };
                break;

        }
        return actions;


    }

}