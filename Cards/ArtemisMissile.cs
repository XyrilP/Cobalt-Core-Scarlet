using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class ArtemisMissile : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "ArtemisMissile", "name"]).Localize,
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
                    data.cost = 0;
                    data.exhaust = true;
                    data.retain = true;
                    break;
                case Upgrade.B:
                    data.cost = 1;
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
                    }
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
                            missileType = MissileType.normal
                        }
                    }
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
                            missileType = MissileType.heavy
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
        }
        return actions;

    }

}