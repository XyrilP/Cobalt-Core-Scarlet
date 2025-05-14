using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class Backstab : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/Backstab.png"));
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Backstab", "name"]).Localize,
            Art = BaseArt?.Sprite
        }
        );
    }
    public override CardData GetData(State state)
    {
        var fadeValue = state.ship.Get(VionheartScarlet.Instance.Fade.Status);
        CardData data = new CardData();
        {
            switch (upgrade)
            {
                case Upgrade.None:
                data.cost = 2;
                data.exhaust = true;
                data.retain = true;
                data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "Backstab", "description"], new { Damage = GetDmg(state, 1 + fadeValue) });
                    break;
                case Upgrade.A:
                data.cost = 2;
                data.retain = true;
                data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "Backstab", "descA"], new { Damage = GetDmg(state, 1 + fadeValue) });
                    break;
                case Upgrade.B:
                data.cost = 2;
                data.exhaust = true;
                data.retain = true;
                data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "Backstab", "descB"], new { Damage = GetDmg(state, 1 + ( 2 * fadeValue)) });
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