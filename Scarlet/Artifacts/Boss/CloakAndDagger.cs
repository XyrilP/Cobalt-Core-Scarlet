using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Artifacts;

public class CloakAndDagger : Artifact, IRegisterable
{
    bool usedCloakAndDagger = false;
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
        var ship = state.ship;
        ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
        if (state.ship.Get(VionheartScarlet.Instance.Fade.Status) > 0 && !usedCloakAndDagger)
        {
            combat.QueueImmediate
            (
                new ADummyAction
                {
                    dialogueSelector = ".CloakAndDaggerTrigger"
                }
            );
            VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "fadeUsedThisTurn", true);
            usedCloakAndDagger = true;
            return true;
        }
        else
        {
            return null;
        }
    }
    // public override int ModifyBaseDamage(int baseDamage, Card? card, State state, Combat? combat, bool fromPlayer)
    // {
    //     Ship ship = state.ship;
    //     if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0 && fromPlayer)
    //     {
    //         return 1;
    //     }
    //     return 0;
    // }
    public override void OnReceiveArtifact(State state)
    {
        VionheartScarlet.Instance.Helper.ModData.SetModData(state.ship, "hasCloakAndDagger", true);
    }
    public override void OnRemoveArtifact(State state)
    {
        VionheartScarlet.Instance.Helper.ModData.SetModData(state.ship, "hasCloakAndDagger", false);
    }
}