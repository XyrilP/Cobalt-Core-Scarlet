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
                    data.flippable = true;
                    break;
            }
            data.cost = 1;
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
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true,
                        dialogueSelector = ".scarletSneakAttack"
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        isRandom = true,
                        dialogueSelector = ".scarletSneakAttack"
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
                        piercing = true,
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletSneakAttack"
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                    },
                    new AStatus()
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 1,
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