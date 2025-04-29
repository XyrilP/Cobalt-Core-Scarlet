using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace Vionheart.Artifacts;

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
                owner = Vionheart.Instance.Scarlet_Deck.Deck
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "HardlightAfterburners", "name"]).Localize,
            Description = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "HardlightAfterburners", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/hardlight_afterburners.png")).Sprite
        }
        );
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        counter++;
        if (counter >= GOAL)
        {
            combat.Queue(new AStatus
            {
                status = Status.hermes,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key(),
                dialogueSelector = ".HardlightAfterburnersTrigger"
            }
            );
            counter = 0;
        }
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        counter = 0;
    }

    public override int? GetDisplayNumber(State s)
    {
        if (counter == 0)
        {
            return null;
        }
        return counter;
    }
}