using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class Sneak : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck, //Which deck should this card go to?
                rarity = Rarity.common, //What rarity should this card be?
                dontOffer = false, //Should this card be offered to the player?
                upgradesTo = [Upgrade.A, Upgrade.B] //Does this card upgrade? and if it has an A or B upgrade.
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Sneak", "name"]).Localize,
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
                    break;
                case Upgrade.A:
                    break;
                case Upgrade.B:
                    break;
            }
            data.cost = 1;
            data.flippable = true;
        }
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        var isFaded = s.ship is Ship ship && IsFaded(ship);
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = isFaded
                    },
                    new ADummyAction()
                    {
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !isFaded
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        disabled = !isFaded
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = isFaded
                    },
                    new ADummyAction()
                    {
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 2,
                        targetPlayer = true,
                        disabled = !isFaded
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                        disabled = !isFaded
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !isFaded
                    },
                    new ADummyAction()
                    {
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = isFaded
                    },
                    new AMove()
                    {
                        dir = 4,
                        targetPlayer = true,
                        disabled = isFaded
                    }
                };
                break;
        }
        return actions;
    }

    public bool IsFaded(Ship ship)
    {
        if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0)
        {
            return true;
        }
        else return false;
    }

}