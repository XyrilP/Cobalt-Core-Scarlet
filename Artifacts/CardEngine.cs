using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;

namespace XyrilP.VionheartScarlet.Artifacts;

public class CardEngine : Artifact, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "CardEngine", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "CardEngine", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/card_engine_2.png")).Sprite
        }
        );
    }

    public int counter;
    private const int GOAL = 10;
    public override int? GetDisplayNumber(State s)
    {
        return counter;
    }

    public override void OnDrawCard(State state, Combat combat, int count)
    {
        counter += count;
        if (counter >= GOAL)
        {
            combat.QueueImmediate(
            [
                new ADrawCard()
                {
                    count = 1,
                    artifactPulse = Key()
                }
            ]
            );
            counter -= GOAL;
        }
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        counter = 0;
        if (combat.turn > 1)
        {
            combat.QueueImmediate(
                [
                    new ADrawCard()
                    {
                        count = 1,
                        artifactPulse = Key()
                    }
                ]
            );
        }
    }
    public override void OnCombatEnd(State state)
    {
        counter = 0;
    }
}