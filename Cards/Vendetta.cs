using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class Vendetta : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Vendetta", "name"]).Localize,
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
                    data.cost = 2;
                    data.flippable = true;
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
        var isFaded = s.ship is Ship ship && IsFaded(ship);

        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        weaken = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 3),
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        weaken = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 3),
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        brittle = true,
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 4),
                    }
                };
                break;

        }
        return actions;


    }

    public bool IsFaded(Ship ship)
    {
        if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0)
        {
            return true;
        }
        else return false;
    }

}