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
            Art = null
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 1
            },
            Upgrade.A => new CardData
            {
                cost = 1
            },
            Upgrade.B => new CardData
            {
                cost = 1,
                flippable = true
            },
            _ => new CardData{}
        };
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None =>
            [
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                        targetPlayer = false
                    }
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                }
            ],
            Upgrade.A =>
            [
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
                }
            ],
            Upgrade.B =>
            [
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                        targetPlayer = false
                    }
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}