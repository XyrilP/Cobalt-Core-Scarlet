using System;
using System.Collections.Generic;
using FSPRO;
using Nickel;
using VionheartScarlet.Cards;

namespace VionheartScarlet.Actions;

public class ABackstab : CardAction
{
    public int damage;
    public override void Begin(G g, State s, Combat c)
    {
        var statusFadeAmount = s.ship.Get(VionheartScarlet.Instance.Fade.Status);
        var backstabDamage = damage + statusFadeAmount;
        c.QueueImmediate(
            new AAttack()
            {
                damage = backstabDamage,
                piercing = true
            }
        );
    }
    public override Icon? GetIcon(State s)
    {
        var statusFadeAmount = s.ship.Get(VionheartScarlet.Instance.Fade.Status);
        var backstabDamage = damage + statusFadeAmount;
        return new Icon(VionheartScarlet.Instance.Backstab_Icon.Sprite, backstabDamage, Colors.redd, false);
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> tooltips =
        [
            new GlossaryTooltip("actionTooltip.ABackstab")
            {
                Icon = VionheartScarlet.Instance.Backstab_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "ABackstab", "name"]),
                Description = VionheartScarlet.Instance.Localizations.Localize(["action", "ABackstab", "description"])
            },
        ];
        return tooltips;
    }
}