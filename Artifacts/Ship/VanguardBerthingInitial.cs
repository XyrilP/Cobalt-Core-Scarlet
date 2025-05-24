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
                pools = [ArtifactPool.Unreleased],
                owner = Deck.colorless
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthingInitial", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthingInitial", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/vanguard_berthing.png")).Sprite
        }
        );
        
    }
    public override void OnReceiveArtifact(State state)
    {
        /* Add Crew Member */
        List<Deck> list = (from dt in state.storyVars.GetUnlockedChars()
        where !state.characters.Any((Character ch) => ch.deckType == (Deck?)dt)
        select dt).ToList();
        if (state.characters.Count < list.Count && !characterAdded)
        {
            Rand rng = new Rand(state.rngCurrentEvent.seed + 3629);
            Deck foundCharacter = Extensions.Random(list, rng);
            state.GetCurrentQueue().Add((CardAction)new AAddCharacter
		    {
                deck = foundCharacter,
                addTheirStarterCardsAndArtifacts = true,
                canGoPastTheCharacterLimit = true
		    }
            );
            characterAdded = true;
            // int berthingCardDraw = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(state.ship, "berthingCardDraw", 0);
            // VionheartScarlet.Instance.Helper.ModData.SetModData(state.ship, "berthingCardDraw", berthingCardDraw + 1);
            Artifact _ = state.EnumerateAllArtifacts().First((Artifact _) => _.Key() == new VanguardBerthing().Key());
            if (_ is VanguardBerthing shipArtifact)
            {
                shipArtifact.berthingInitialized = true;
                shipArtifact.berthingCardDraw++;
            }
        }
        /* Add Crew Member */
        /* Remove this Artifact */
        state.GetCurrentQueue().Add((CardAction)
            new ALoseArtifact
            {
                artifactType = Key()
            }
        );
        /* Remove this Artifact */
    }
}