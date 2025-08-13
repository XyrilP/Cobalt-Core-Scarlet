using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Artifacts;

public class HardlightAfterburners : Artifact, IRegisterable
{
    public int counter;
    private const int GOAL = 2;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Boss],
                owner = VionheartScarlet.Instance.Scarlet_Deck.Deck
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "HardlightAfterburners", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "HardlightAfterburners", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/hardlight_afterburners.png")).Sprite
        }
        );
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        counter++;
        if (counter > GOAL)
        {
            combat.Queue
            (
                new AInstantTrick
                {
                    amount = 1,
                    artifactPulse = Key(),
                    dialogueSelector = ".HardlightAfterburnersTrigger"
                }
            );
            counter = 1;
        }
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        counter = 0;
        combat.Queue
        (
            new AInstantTrick
            {
                amount = 1,
                artifactPulse = Key(),
                dialogueSelector = ".HardlightAfterburnersTrigger"
            }
        );
    }

    public override int? GetDisplayNumber(State s)
    {
        if (counter == 0)
        {
            return null;
        }
        return counter;
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> tooltips =
        [
            new GlossaryTooltip("actionTooltip.AInstantTrick")
            {
                Icon = VionheartScarlet.Instance.InstantTrick_Icon.Sprite,
                TitleColor = Colors.action,
                Title = VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "name"]),
                Description = string.Format(VionheartScarlet.Instance.Localizations.Localize(["action", "AInstantTrick", "description"]), 1)
            },
        ];
        return tooltips;
    }
}