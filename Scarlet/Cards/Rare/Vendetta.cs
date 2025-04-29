using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class Vendetta : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "Vendetta", "name"]).Localize,
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 2,
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                cost = 2,
                exhaust = true,
                flippable = true
            },
            Upgrade.B => new CardData
            {
                cost = 3,
                exhaust = true
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
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                    weaken = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 3),
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                    weaken = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 3),
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                    brittle = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 4),
                }
            ],
            _ => []
        };
    }
}