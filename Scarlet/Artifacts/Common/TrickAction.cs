using System.Net.Http.Headers;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Artifacts;

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
}