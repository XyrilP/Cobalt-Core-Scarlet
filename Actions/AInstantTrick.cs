using System;
using System.Collections.Generic;
using System.Linq;
using FSPRO;
using Nickel;
using VionheartScarlet.Artifacts;
using VionheartScarlet.Cards;

namespace VionheartScarlet.Actions;

public class AInstantTrick : CardAction
{
    private static Card trickCard = new DriftLeft();
    public int amount = 1;
    public bool isTeleport = false;
    private static List<CardAction> trickActionList = [];
    public override void Begin(G g, State s, Combat c)
    {
        for (int i = amount; i > 0; i--)
        {
            var rng = s.rngActions.Next();
            if (rng >= 0 && rng < 0.25) { trickCard = new DriftLeft(); }
            else if (rng >= 0.25 && rng < 0.50) { trickCard = new DriftRight(); }
            // else if (rng >= 0.50 && rng < 0.75) { trickCard = new TrickAfterburn(); }
            // else if (rng >= 0.50 && rng < 0.75) { trickCard = new Freestyle(); }
            else if (rng >= 0.50 && rng < 0.75) { trickCard = new TrickBarrelRoll(); }
            else if (rng >= 0.75 && rng < 0.85) { trickCard = new Trickstab(); }
            //else if (rng >= 0.75 && rng < 0.85) { trickCard = new MantaDodge(); }
            else if (rng >= 0.85 && rng < 0.95) { trickCard = new Veer(); }
            else if (rng >= 0.95) { trickCard = new Flicker(); }
            Artifact _ = s.EnumerateAllArtifacts().First((Artifact _) => _.Key() == new TrickAction().Key());
            if (_ is TrickAction artifact)
            {
                rng = s.rngActions.Next();
                if (rng >= 0 && rng < 0.33) { trickCard.upgrade = Upgrade.None; }
                else if (rng >= 0.33 && rng < 0.66 && artifact.tricksterUpgrade) { trickCard.upgrade = Upgrade.A; artifact.tricksterUpgrade = false; artifact.Pulse(); }
                else if (rng > 0.66 && artifact.tricksterUpgrade) { trickCard.upgrade = Upgrade.B; artifact.tricksterUpgrade = false; artifact.Pulse(); }
            }
            trickActionList.AddRange(trickCard.GetActions(s, c));
            if (isTeleport) // Make AMove actions make isTeleport true
            {
                for (int j = trickActionList.Count; j > 0; j--)
                {
                    if (trickActionList[j - 1] is AMove action)
                    {
                        action.isTeleport = true;
                    }
                }
            }
            if (trickCard.GetData(s).flippable || trickCard.GetData(s).floppable) // If card has flippable, randomly determine if it is flipped or not
            {
                rng = s.rngActions.Next();
                if (rng < 0.5) { trickCard.flipped = true; }
                else { trickCard.flipped = false; }
            }
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
                Icon = VionheartScarlet.Instance.InstantTrick_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "name"]),
                Description = string.Format(VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "description"]), $"{amount}")
            },
            // new TTCard { card = new TrickAfterburn() },
            // new TTCard { card = new DriftLeft() },
            // new TTCard { card = new DriftRight() },
            // new TTCard { card = new MantaDodge() },
            // new TTCard { card = new Veer() },
            // new TTCard { card = new Flicker() }
        ];
        return tooltips;
    }
}