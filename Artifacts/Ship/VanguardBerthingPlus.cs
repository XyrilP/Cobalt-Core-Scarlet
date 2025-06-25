using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using HarmonyLib;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Artifacts;

public class VanguardBerthingPlus : Artifact, IRegisterable
{
    bool characterAdded { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.Boss],
                owner = Deck.colorless
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthingPlus", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthingPlus", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/vanguard_berthing_plus.png")).Sprite
        }
        );
        
    }
    public override void OnReceiveArtifact(State state)
    {
        /* Add Crew Member V1 */
        // List<Deck> list = (from dt in state.storyVars.GetUnlockedChars()
        // where !state.characters.Any((Character ch) => ch.deckType == (Deck?)dt)
        // select dt).ToList();
        // if (state.characters.Count < list.Count && !characterAdded)
        // {
        //     Rand rng = new Rand(state.rngCurrentEvent.seed + 3629);
        //     Deck foundCharacter = Extensions.Random(list, rng);
        //     state.GetCurrentQueue().Add((CardAction)new AAddCharacter
        //     {
        //         deck = foundCharacter,
        //         addTheirStarterCardsAndArtifacts = true,
        //         canGoPastTheCharacterLimit = true
        //     }
        //     );
        //     characterAdded = true;
        //     Artifact _ = state.EnumerateAllArtifacts().First((Artifact _) => _.Key() == new VanguardBerthing().Key());
        //     if (_ is VanguardBerthing shipArtifact)
        //     {
        //         shipArtifact.berthingCardDraw++;
        //         shipArtifact.berthingEnergyFragment++;
        //         shipArtifact.Pulse();
        //     }
        // }
        /* Add Crew Member V1 */
        /* Add Crew Member V2 */
        Artifact _ = state.EnumerateAllArtifacts().First((Artifact _) => _.Key() == new VanguardBerthing().Key());
        if (_ is VanguardBerthing shipArtifact)
        {
            state.GetCurrentQueue().Add((CardAction)new AVanguardBerthing
            {
                choiceAmount = 3,
                upgradedStarters = true,
                addUncommonCards = 2,
                addRareCards = 1
            }
            );
            shipArtifact.berthingCardDraw++;
            shipArtifact.berthingEnergyFragment++;
            shipArtifact.Pulse();
        }
        /* Add Crew Member V2 */
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