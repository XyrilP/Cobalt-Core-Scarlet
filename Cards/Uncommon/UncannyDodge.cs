using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class UncannyDodge : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "UncannyDodge", "name"]).Localize,
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
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                retain = true
            },
            Upgrade.B => new CardData
            {
                cost = 2,
                recycle = true
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
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}