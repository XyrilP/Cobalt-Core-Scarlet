using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletVeer : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("Veer", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Veer", "name"]).Localize
        
        }
        );

    }

    public override CardData GetData(State state)
    {

        CardData data = new CardData();
        {

            data.cost = 1;
            switch (upgrade)
            {
                case Upgrade.None:
                    data.flippable = true;
                    break;
                case Upgrade.A:
                    data.flippable = false;
                    break;
                case Upgrade.B:
                    data.flippable = false;
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
                        dir = 2,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    { 
                        dir = -2,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.autododgeLeft,
                        statusAmount = 1,
                        targetPlayer = true

                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true
                    },
                    new AStatus()
                    {

                        status = Status.autododgeRight,
                        statusAmount = 1,
                        targetPlayer = true

                    },
                };
                break;

        }
        return actions;


    }

}