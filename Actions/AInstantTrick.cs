using System;
using System.Collections.Generic;
using System.Linq;
using FSPRO;
using Nickel;
using VionheartScarlet.Cards;

namespace VionheartScarlet.Actions;

public class AInstantTrick : CardAction
{
    private static Card trickCard = new DriftLeft();
    public int amount = 1;
    private static List<CardAction> trickActionList = [];
    public override void Begin(G g, State s, Combat c)
    {
        for (int i = amount; i > 0; i--)
        {
            var rng = s.rngActions.Next();
            if (rng >= 0 && rng < 0.2) { trickCard = new DriftLeft(); }
            else if (rng >= 0.2 && rng < 0.4) { trickCard = new DriftRight(); }
            else if (rng >= 0.4 && rng < 0.6) { trickCard = new TrickAfterburn(); }
            else if (rng >= 0.6 && rng < 0.75) { trickCard = new MantaDodge(); }
            else if (rng >= 0.75 && rng < 0.9) { trickCard = new Veer(); }
            else if (rng >= 0.90) { trickCard = new Flicker(); }
            trickActionList.AddRange(trickCard.GetActions(s, c));
            c.QueueImmediate(trickActionList);
            trickActionList.Clear();
        }
    }
    public override Icon? GetIcon(State s)
    {
        return new Icon(VionheartScarlet.Instance.InstantTrick_Icon.Sprite, amount, Colors.textMain, false);
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> tooltips =
        [
            new GlossaryTooltip("actionTooltip.AInstantTrick")
            {
                Icon = VionheartScarlet.Instance.Trick_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "name"]),
                Description = string.Format(VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "description"]), $"{amount}")
            },
            new TTCard { card = new TrickAfterburn() },
            new TTCard { card = new DriftLeft() },
            new TTCard { card = new DriftRight() },
            new TTCard { card = new MantaDodge() },
            new TTCard { card = new Veer() },
            new TTCard { card = new Flicker() },
        ];
        return tooltips;
    }
}