using System;
using System.Collections.Generic;
using FSPRO;
using Nickel;
using VionheartScarlet.Cards;

namespace VionheartScarlet.Actions;

public class ATrickDraw : CardAction
{
    private static Card trickCard = new DriftLeft();
    public int amount = 1;
    public override void Begin(G g, State s, Combat c)
    {
        for (int i = amount; i > 0; i--)
        {
            var rng = s.rngActions.Next();
            if (rng >= 0 && rng < 0.25) { trickCard = new DriftLeft(); }
            else if (rng >= 0.25 && rng < 0.50) { trickCard = new DriftRight(); }
            // else if (rng >= 0.50 && rng < 0.75) { trickCard = new TrickAfterburn(); }
            else if (rng >= 0.50 && rng < 0.75) { trickCard = new Freestyle(); }
            else if (rng >= 0.75 && rng < 0.85) { trickCard = new Trickstab(); }
            //else if (rng >= 0.75 && rng < 0.85) { trickCard = new MantaDodge(); }
            else if (rng >= 0.85 && rng < 0.95) { trickCard = new Veer(); }
            else if (rng >= 0.95) { trickCard = new Flicker(); }
            c.QueueImmediate(
                new AAddCard
                {
                    card = trickCard,
                    destination = CardDestination.Hand,
                    amount = 1
                }
            );
        }
    }
    public override Icon? GetIcon(State s)
    {
        return new Icon(VionheartScarlet.Instance.Trick_Icon.Sprite, amount, Colors.textMain, false);
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> tooltips =
        [
            new GlossaryTooltip("actionTooltip.ATrickDraw")
            {
                Icon = VionheartScarlet.Instance.Trick_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "ATrickDraw", "name"]),
                Description = string.Format(VionheartScarlet.Instance.Localizations.Localize(["action", "ATrickDraw", "description"]), $"{amount}")
            },
            // new TTCard { card = new TrickAfterburn() },
            // new TTCard { card = new DriftLeft() },
            // new TTCard { card = new DriftRight() },
            // new TTCard { card = new MantaDodge() },
            // new TTCard { card = new Veer() },
            // new TTCard { card = new Flicker() },
        ];
        return tooltips;
    }
}