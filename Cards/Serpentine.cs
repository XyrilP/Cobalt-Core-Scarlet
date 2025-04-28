using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class Serpentine : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Serpentine", "name"]).Localize,
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
                exhaust = true
            },
            Upgrade.B => new CardData
            {
                cost = 2,
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
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 4,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 4,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 4,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 5,
                    targetPlayer = true,
                    isRandom = true
                }
            ],
            _ => []
        };
    }
}