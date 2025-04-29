using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;

namespace Vionheart.Artifacts;

public class Foresight : Artifact, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.EventOnly],
                owner = Vionheart.Instance.Sweetroll_Deck.Deck,
                unremovable = true
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "Foresight", "name"]).Localize,
            Description = Vionheart.Instance.AnyLocalizations.Bind(["artifact", "Foresight", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/Foresight.png")).Sprite
        }
        );
    }
    public override void OnCombatStart(State state, Combat combat)
    {
        if (combat.turn == 1)
        {
            combat.QueueImmediate(
            [
                new ACardSelect
                {
                    browseAction = new ChooseCardToPutInHand
                    {
                    },
                    browseSource = CardBrowse.Source.DrawPile
                }
            ]
            );
        }
    }
}