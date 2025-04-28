using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class BlinkStrike : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "BlinkStrike", "name"]).Localize,
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 1,
                flippable = true
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                flippable = true
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
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    dialogueSelector = ".scarletBlinkStrike"
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    dialogueSelector = ".scarletBlinkStrike"
                },
                new AAttack
                {
                    damage = GetDmg(s, 2),
                    piercing = true
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = -2,
                    targetPlayer = true,
                    dialogueSelector = ".scarletBlinkStrike"
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true
                },
                new AMove
                {
                    dir = 4,
                    targetPlayer = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true
                }
            ],
            _ => []
        };
    }
}