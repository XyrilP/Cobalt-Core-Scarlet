using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Artifacts;

public class TrickAction : Artifact, IRegisterable
{
    public bool tricksterUpgrade;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = VionheartScarlet.Instance.Scarlet_Deck.Deck
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "TrickAction", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "TrickAction", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/trick_action.png")).Sprite
        }
        );
    }

    public override void OnTurnEnd(State state, Combat combat)
    {
        var ship = state.ship;
        var energyValue = combat.energy;
        if (energyValue > 0)
        {
            combat.Queue(
                new ATrickDraw()
                {
                    amount = 1,
                    artifactPulse = Key(),
                    dialogueSelector = ".TrickActionTrigger"
                }
            );
        }
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        tricksterUpgrade = true;
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> tooltips =
        [
            new GlossaryTooltip("actionTooltip.ATrickDraw")
            {
                Icon = VionheartScarlet.Instance.Trick_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "ATrickDraw", "name"]),
                Description = string.Format(VionheartScarlet.Instance.Localizations.Localize(["action", "ATrickDraw", "description"]), $"{1}")
            },
            new GlossaryTooltip("actionTooltip.AInstantTrick")
            {
                Icon = VionheartScarlet.Instance.InstantTrick_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "name"]),
                Description = string.Format(VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "description"]), $"{1}")
            }
        ];
        return tooltips;
    }
}