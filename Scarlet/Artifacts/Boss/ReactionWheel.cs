using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Artifacts;

public class ReactionWheel : Artifact, IRegisterable
{
    private static Spr spriteOn;

    private static Spr spriteOff;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        spriteOn = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/reaction_wheel.png")).Sprite;
        spriteOff = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/reaction_wheel_off.png")).Sprite;
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Boss],
                owner = VionheartScarlet.Instance.Scarlet_Deck.Deck
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "ReactionWheel", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "ReactionWheel", "description"]).Localize,
            Sprite = spriteOn
        }
        );
    }

    private const int cap = 2;
    public int counter;
    private const int GOAL = 10;
    public int capProgress;
    public override int? GetDisplayNumber(State s)
    {
        return counter;
    }

    public override Spr GetSprite()
    {
        if (capProgress >= cap)
        {
            return spriteOff;
        }
        return spriteOn;
    }

    public override void OnDrawCard(State state, Combat combat, int count)
    {
        counter += count;
        if (counter >= GOAL && capProgress < cap)
        {
            combat.QueueImmediate(
            [
                new AStatus()
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true,
                    artifactPulse = Key(),
                    dialogueSelector = ".ReactionWheelTrigger"
                }
            ]
            );
            counter -= GOAL;
            capProgress++;
        }
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        capProgress = 0;
    }
    public override void OnCombatEnd(State state)
    {
        capProgress = 0;
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        var evadeStatus = Status.evade;
        List<Tooltip> tooltips = [];
        tooltips.AddRange(StatusMeta.GetTooltips(evadeStatus, 1));
        return tooltips;
    }
}