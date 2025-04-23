using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class Backstab : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Backstab", "name"]).Localize,
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
                data.exhaust = true;
                data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "Backstab", "description"]);
                    break;
                case Upgrade.A:
                data.cost = 1;
                data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "Backstab", "descA"]);
                    break;
                case Upgrade.B:
                data.cost = 2;
                data.exhaust = true;
                data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "Backstab", "descB"]);
                    break;
            }
        }
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s,1) + s.ship.Get(VionheartScarlet.Instance.Fade.Status),
                        piercing = true,
                        stunEnemy = true
                    },
                    new AStatus
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s,1) + s.ship.Get(VionheartScarlet.Instance.Fade.Status),
                        piercing = true,
                        stunEnemy = true
                    },
                    new AStatus
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s,1) + (s.ship.Get(VionheartScarlet.Instance.Fade.Status) * 2),
                        piercing = true,
                        stunEnemy = true
                    },
                    new AStatus
                    {
                        status = VionheartScarlet.Instance.Fade.Status,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}