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

            data.cost = 2;
            switch (upgrade)
            {
                case Upgrade.A:
                    data.flippable = true;
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
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        weaken = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
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
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        weaken = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
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
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        brittle = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
                    }
                };
                break;

        }
        return actions;


    }

}