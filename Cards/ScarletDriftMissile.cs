using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletDriftMissile : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("DriftMissile", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "DriftMissile", "name"]).Localize
        
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
                    new AMove()
                    {
                        dir = 3,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.seeker
                        }
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 3,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.seeker
                        }
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 4,
                        targetPlayer = true,
                        isRandom = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.seeker
                        }
                    },
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.seeker
                        },
                        offset = 2
                    }
                };
                break;
        }
        return actions;

    }

}