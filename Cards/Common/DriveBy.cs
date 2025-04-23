using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class DriveBy : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "DriveBy", "name"]).Localize,
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
                cost = 2
            },
            Upgrade.A => new CardData
            {
                cost = 1
            },
            Upgrade.B => new CardData
            {
                cost = 2,
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
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                }
            ],
            Upgrade.A =>
            [
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                }
            ],
            Upgrade.B =>
            [
                new AAttack
                {
                    damage = GetDmg(s, 2)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 2)
                }
            ],
            _ => []
        };
    }
}