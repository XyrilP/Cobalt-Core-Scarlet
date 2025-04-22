using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using XyrilP.VionheartScarlet.Midrow;

namespace XyrilP.VionheartScarlet.Cards;

public class TrickDaggerCard : Card, IRegisterable
{

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {

        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "TrickDagger_Missile", "name"]).Localize,
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
                    data.flippable = true;
                    break;
            }
            data.cost = 1;
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
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                            targetPlayer = false
                        }
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                            targetPlayer = false
                        }
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                            targetPlayer = false
                        }
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn()
                    {
                        thing = new TrickDagger_Missile
                        {
                            targetPlayer = false
                        }
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