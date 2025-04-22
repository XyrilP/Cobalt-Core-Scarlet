using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace XyrilP.VionheartScarlet.Artifacts;

public class CloakAndDagger : Artifact, IRegisterable
{
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "CloakAndDagger", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "CloakAndDagger", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak_and_dagger.png")).Sprite
        }
        );
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        state.ship.Add(VionheartScarlet.Instance.Fade.Status, 1);
        combat.Queue(new ADummyAction
        {
            artifactPulse = Key()
        }
        );
    }

    public override bool? OnPlayerAttackMakeItPierce(State state, Combat combat)
    {
        if (state.ship.Get(VionheartScarlet.Instance.Fade.Status) > 0)
        {
            combat.QueueImmediate
            (
                new ADummyAction
                {
                    dialogueSelector = ".CloakAndDaggerTrigger"
                }
            );
            return true;
        }
        return null;
    }
}