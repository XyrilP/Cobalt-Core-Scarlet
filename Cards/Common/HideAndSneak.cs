using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class HideAndSneak : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "HideAndSneak", "name"]).Localize,
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
                    break;
                case Upgrade.B:
                    data.cost = 1;
                    break;
            }
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
                        dialogueSelector = ".scarletHideAndSneak"
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
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
                        dialogueSelector = ".scarletHideAndSneak"
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 2,
                        targetPlayer = true,
                        dialogueSelector = ".scarletHideAndSneak"
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
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