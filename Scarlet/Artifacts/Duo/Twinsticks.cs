using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Artifacts;

public class Twinsticks : Artifact, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        if (VionheartScarlet.Instance.DuoArtifactsApi is not { } api) return; //Do not register if Duo artifacts are not installed.

        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = VionheartScarlet.Instance.DuoArtifactsApi.DuoArtifactVanillaDeck
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "Twinsticks", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "Twinsticks", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/twinsticks.png")).Sprite
        }
        );

        var duoCharacter1 = VionheartScarlet.Instance.Scarlet_Deck.Deck;
        var duoCharacter2 = Deck.riggs;
        api.RegisterDuoArtifact(MethodBase.GetCurrentMethod()!.DeclaringType!, [duoCharacter1, duoCharacter2]);
    }
    public int turnCounter;

    public override int? GetDisplayNumber(State s)
    {
        if (turnCounter <= 0 || turnCounter > 2) return null;
        return turnCounter;
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        Ship ship = state.ship;
        turnCounter++;
        if (turnCounter == 1)
        {
            combat.QueueImmediate
            (
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true,
                    artifactPulse = Key()
                }
            );
        }
        if (turnCounter == 2)
        {
            combat.QueueImmediate
            (
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    artifactPulse = Key()
                }
            );
        }
    }

    public override void OnTurnEnd(State state, Combat combat)
    {
        if (turnCounter >= 2) turnCounter = 0;
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        if (turnCounter >= 2) turnCounter = 0;
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        var evadeStatus = Status.evade;
        var fadeStatus = VionheartScarlet.Instance.Fade.Status;
        List<Tooltip> tooltips = [];
        tooltips.AddRange(StatusMeta.GetTooltips(evadeStatus, 1));
        tooltips.AddRange(StatusMeta.GetTooltips(fadeStatus, 1));
        return tooltips;
    }
}