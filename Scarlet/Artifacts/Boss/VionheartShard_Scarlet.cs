using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace Vionheart.Artifacts;

public class VionheartShard_Scarlet : Artifact, IRegisterable
{
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
            Name = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "VionheartShard_Scarlet", "name"]).Localize,
            Description = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "VionheartShard_Scarlet", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/vionheart_shard.png")).Sprite
        }
        );
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        state.ship.Add(Vionheart.Instance.Fade.Status, 4);
        combat.Queue(new ADummyAction
        {
            artifactPulse = Key()
        }
        );
    }
}