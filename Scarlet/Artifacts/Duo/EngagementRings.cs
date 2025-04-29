using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using XyrilP.ExternalAPI;

namespace Vionheart.Artifacts;

public class EngagementRings : Artifact, IRegisterable
{
    public static IDeckEntry? EvilRiggs_Deck { get; set; }
    public static IStatusEntry? EvilRiggs_Rage { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        if (Vionheart.Instance.DuoArtifactsApi is not { } api) return; //Do not register if Duo artifacts are not installed.
        EvilRiggs_Deck = helper.Content.Decks.LookupByUniqueName("parchment.evilRiggs.manifest::parchment.evilRiggs.evilRiggsDeck");
        if (EvilRiggs_Deck == null)
        {
            Vionheart.Instance.Logger.LogInformation("True Riggs isn't real...");
            return;
        }
        Vionheart.Instance.Logger.LogInformation("True Riggs detected!");
        EvilRiggs_Rage = helper.Content.Statuses.LookupByUniqueName("parchment.evilRiggs.manifest::parchment.evilRiggs.rage");

        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = Vionheart.Instance.DuoArtifactsApi.DuoArtifactVanillaDeck
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "EngagementRings", "name"]).Localize,
            Description = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "EngagementRings", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/engagement_rings.png")).Sprite
        }
        );

        var duoCharacter1 = Vionheart.Instance.Scarlet_Deck.Deck;
        var duoCharacter2 = EvilRiggs_Deck.Deck;
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
        if (EvilRiggs_Rage == null) return;
        turnCounter++;
        if (turnCounter == 1)
        {
            combat.QueueImmediate
            (
                new AStatus
                {
                    status = EvilRiggs_Rage.Status,
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
                    status = Vionheart.Instance.scarletBarrage.Status,
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
}