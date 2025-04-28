using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;

namespace XyrilP.VionheartScarlet.Artifacts;

public class TwoPilots : Artifact, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.EventOnly],
                owner = Deck.colorless,
                unremovable = true
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "TwoPilots", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "TwoPilots", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/two_pilots.png")).Sprite
        }
        );
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        if (combat.turn == 1)
        {
            combat.QueueImmediate(
            [
                new AEnergy
                {
                    changeAmount = 1,
                    artifactPulse = Key()
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new ADrawCard
                {
                    count = 1
                },
                new AStatus
                {
                    status = Status.drawNextTurn,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ]
            );
        }
    }
}