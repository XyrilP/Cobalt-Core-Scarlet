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
    private static Spr spriteCloakAndDagger;
    private static Spr spriteCloakAnd;
    private static Spr spriteAndDagger;
    private static Spr spriteAnd;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        spriteCloakAndDagger = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak_and_dagger.png")).Sprite;
        spriteCloakAnd = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak_and_dagger-off.png")).Sprite;
        spriteAndDagger = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak-off_and_dagger.png")).Sprite;
        spriteAnd = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/cloak-off_and_dagger-off.png")).Sprite;
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
        if (!usedCloakAndDagger_Fade && !usedCloakAndDagger_Attack)
        {
            return spriteCloakAndDagger;
        }
        else if (usedCloakAndDagger_Fade && !usedCloakAndDagger_Attack)
        {
            return spriteAndDagger;
        }
        else if (!usedCloakAndDagger_Fade && usedCloakAndDagger_Attack)
        {
            return spriteCloakAnd;
        }
        else return spriteAnd;
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
    public override List<Tooltip>? GetExtraTooltips()
    {
        var fadeStatus = VionheartScarlet.Instance.Fade.Status;
        List<Tooltip> tooltips = [];
        tooltips.AddRange(StatusMeta.GetTooltips(fadeStatus, 1));
        return tooltips;
    }
    public override void OnCombatEnd(State state)
    {
        usedCloakAndDagger_Fade = false;
        usedCloakAndDagger_Attack = false;
    }
}