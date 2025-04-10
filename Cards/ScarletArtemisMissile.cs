using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

internal sealed class ScarletArtemisMissile : Card, IScarletCard
{

    public static void Register(IModHelper helper)
    {

        helper.Content.Cards.RegisterCard("ArtemisMissile", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]

            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "ArtemisMissile", "name"]).Localize
        
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
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.normal
                        }
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.heavy
                        }
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.normal
                        }
                    },
                    new ASpawn()
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.normal
                        },
                        offset = 1
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        isRandom = true
                    },
                };
                break;
        }
        return actions;

    }

}