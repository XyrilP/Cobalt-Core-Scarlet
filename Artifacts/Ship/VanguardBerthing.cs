using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;

namespace VionheartScarlet.Artifacts;

public class VanguardBerthing : Artifact, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthing", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthing", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/artifact_placeholder.png")).Sprite
        }
        );
    }

    public override void OnReceiveArtifact(State state)
    {
        Rand rng = new Rand(state.rngCurrentEvent.seed + 3629);
        Ship ship = state.ship;
        List<Deck> list = (from dt in state.storyVars.GetUnlockedChars()
        where !state.characters.Any((Character ch) => ch.deckType == (Deck?)dt)
        select dt).ToList();
        Deck foundCharacter = Extensions.Random<Deck>(list, rng);
        state.GetCurrentQueue().Add((CardAction)new AAddCharacter
        {
            deck = foundCharacter,
            addTheirStarterCardsAndArtifacts = true,
            canGoPastTheCharacterLimit = true
        }
        );
    }
}