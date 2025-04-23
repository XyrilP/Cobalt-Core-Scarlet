using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class SneakAttack : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "SneakAttack", "name"]).Localize,
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
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    dialogueSelector = ".scarletSneakAttack"
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    dialogueSelector = ".scarletSneakAttack"
                },
                new AAttack
                {
                    damage = GetDmg(s, 2),
                    piercing = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    dialogueSelector = ".scarletSneakAttack"
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ],
            _ => []
        };
    }
}