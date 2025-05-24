using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using HarmonyLib;

namespace VionheartScarlet.Artifacts;

public class VanguardBerthingInitial : Artifact, IRegisterable
{
    bool characterAdded { get; set; }
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthing", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthing", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/vanguard_berthing.png")).Sprite
        }
        );
        
    }
    public override void OnCombatStart(State state, Combat combat)
    {
        combat.QueueImmediate(
        [
            new AAddArtifact
            {
                artifact = new VanguardBerthingOne()
            },
            new ALoseArtifact
            {
                artifactType = Key()
            }
        ]
        );
    }
}