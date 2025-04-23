using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class AdjustThrottle : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "AdjustThrottle", "name"]).Localize,
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
                cost = 1,
                floppable = true,
                retain = true
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                floppable = true,
                retain = true
            },
            Upgrade.B => new CardData
            {
                cost = 1,
                floppable = true,
                retain = true,
                infinite = true
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
                    status = Status.hermes,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = Status.engineStall,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Status.hermes,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = Status.engineStall,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = Status.hermes,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = flipped
                },
                new ADummyAction
                {
                },
                new AStatus
                {
                    status = Status.engineStall,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            _ => []
        };
    }
}