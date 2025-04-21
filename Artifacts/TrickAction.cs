using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace XyrilP.VionheartScarlet.Artifacts;

public class TrickAction : Artifact, IRegisterable
{
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
        if (state.ship.Get(VionheartScarlet.Instance.Fade.Status) == 0)
        {
            state.ship.Add(VionheartScarlet.Instance.Fade.Status, 1);
            combat.Queue(new ADummyAction
            {
                artifactPulse = Key()
            }
            );
        }
    }
}