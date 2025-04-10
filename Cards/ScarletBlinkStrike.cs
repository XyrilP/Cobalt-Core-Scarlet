using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletBlinkStrike : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("BlinkStrike", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "BlinkStrike", "name"]).Localize
        
        }
        );

    }

    public override CardData GetData(State state)
    {

        CardData data = new CardData();
        {
            data.cost = 1;
            data.flippable = true;
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
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
                        piercing = true
                    }
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
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    },
                    new AMove()
                    {
                        dir = -4,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    }
                };
                break;

        }
        return actions;


    }

}