using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Artifacts;

public class CloakAndDagger : Artifact, IRegisterable
{
    bool usedCloakAndDagger;
    private static Spr spriteOn;
    private static Spr spriteOff;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        spriteOn = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak_and_dagger.png")).Sprite;
        spriteOff = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak_and_dagger_off.png")).Sprite;
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
        usedCloakAndDagger = false;
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
        if (state.ship.Get(VionheartScarlet.Instance.Fade.Status) > 0 && !usedCloakAndDagger)
        {
            combat.QueueImmediate
            (
                new ADummyAction
                {
                    dialogueSelector = ".CloakAndDaggerTrigger"
                }
            );
            ship.Add(VionheartScarlet.Instance.Fade.Status, -1);
            VionheartScarlet.Instance.Helper.ModData.SetModData(ship, "fadeUsedThisTurn", true);
            usedCloakAndDagger = true;
            return true;
        }
        else
        {
            return null;
        }
    }
    public override Spr GetSprite()
    {
        if (usedCloakAndDagger)
        {
            return spriteOff;
        }
        return spriteOn;
    }
    public override int ModifyBaseDamage(int baseDamage, Card? card, State state, Combat? combat, bool fromPlayer)
    {
        var ship = state.ship;
        var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);
        if (fadeValue > 0 && fromPlayer && !usedCloakAndDagger)
        {
            return 1;
        }
        return 0;
    }
    public override void OnReceiveArtifact(State state)
    {
        VionheartScarlet.Instance.Helper.ModData.SetModData(state.ship, "hasCloakAndDagger", true);
    }
    public override void OnRemoveArtifact(State state)
    {
        VionheartScarlet.Instance.Helper.ModData.SetModData(state.ship, "hasCloakAndDagger", false);
    }
}