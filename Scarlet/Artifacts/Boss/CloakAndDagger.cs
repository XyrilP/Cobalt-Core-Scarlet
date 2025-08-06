using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Artifacts;

public class CloakAndDagger : Artifact, IRegisterable
{
    bool usedCloakAndDagger_Fade;
    bool usedCloakAndDagger_Attack;
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
        usedCloakAndDagger_Fade = false;
        usedCloakAndDagger_Attack = false;
    }
    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if (status == VionheartScarlet.Instance.Fade.Status && !usedCloakAndDagger_Fade)
        {
            combat.QueueImmediate(
                new AStatus()
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = 1,
                    artifactPulse = Key()
                }
            );
            usedCloakAndDagger_Fade = true;
        }
    }  
    public override bool? OnPlayerAttackMakeItPierce(State state, Combat combat)
    {
        var ship = state.ship;
        if (ship.Get(VionheartScarlet.Instance.Fade.Status) > 0 && !usedCloakAndDagger_Attack)
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
            usedCloakAndDagger_Attack = true;
            return true;
        }
        else
        {
            return null;
        }
    }
    public override Spr GetSprite()
    {
        if (usedCloakAndDagger_Attack)
        {
            return spriteOff;
        }
        return spriteOn;
    }
    public override int ModifyBaseDamage(int baseDamage, Card? card, State state, Combat? combat, bool fromPlayer)
    {
        var ship = state.ship;
        var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);
        if (fadeValue > 0 && fromPlayer && !usedCloakAndDagger_Attack)
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