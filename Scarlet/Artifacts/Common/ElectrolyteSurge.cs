using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace Vionheart.Artifacts;

public class ElectrolyteSurge : Artifact, IRegisterable
{
    private static Spr spriteOn;

    private static Spr spriteOff;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        spriteOn = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/electrolyte_surge.png")).Sprite;
        spriteOff = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/electrolyte_surge_off.png")).Sprite;
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Common],
                owner = Vionheart.Instance.Scarlet_Deck.Deck
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "ElectrolyteSurge", "name"]).Localize,
            Description = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "ElectrolyteSurge", "description"]).Localize,
            Sprite = spriteOn
        }
        );
    }

    private const int cap = 1;
    private const int GOAL = 2;
    public int count;
    public int capProgress;
    public override int? GetDisplayNumber(State s)
    {
        if (capProgress >= cap || count < 0 || count > GOAL)
        {
            return null;
        }
        return count;
    }

    public override Spr GetSprite()
    {
        if (capProgress >= cap)
        {
            return spriteOff;
        }
        return spriteOn;
    }

    public override void OnPlayerDeckShuffle(State state, Combat combat)
    {
        count++;
        if (count >= GOAL && capProgress < cap)
        {
            combat.QueueImmediate(
            [
                new AEnergy()
                {
                    changeAmount = 1,
                    artifactPulse = Key(),
                    dialogueSelector = ".ElectrolyteSurgeTrigger"
                }
            ]
            );
            count = 0;
            capProgress++;
        }
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        capProgress = 0;
    }
}