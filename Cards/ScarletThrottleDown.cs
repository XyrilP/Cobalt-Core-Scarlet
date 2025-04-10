using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletThrottleDown : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("ThrottleDown", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "ThrottleDown", "name"]).Localize
        
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
                    data.cost = 1;
                    break;
                case Upgrade.A:
                    data.cost = 1;
                    break;
                case Upgrade.B:
                    data.cost = 0;
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
                    new AStatus()
                    {
                        status = Status.engineStall,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.engineStall,
                        statusAmount = 2,
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
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;

        }
        return actions;


    }

}