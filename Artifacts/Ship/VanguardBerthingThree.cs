using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using HarmonyLib;

namespace VionheartScarlet.Artifacts;

public class VanguardBerthingThree : Artifact, IRegisterable
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
                owner = Deck.colorless,
                unremovable = true
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthingThree", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthingThree", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/vanguard_berthing_3.png")).Sprite
        }
        );
        
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        /* Add Energy Fragment */
        state.ship.Add(Status.energyFragment, 1);
        while (true)
        {
            int energyFragmentValue = state.ship.Get(Status.energyFragment);
            if (energyFragmentValue >= 3)
            {
                combat.QueueImmediate(
                [
                    new AEnergy
                    {
                        changeAmount = 1
                    }
                ]
                );
                state.ship.Add(Status.energyFragment, -3);
            }
            else
            {
                break;
            }
        }
        /* Add Energy Fragment */
        /* Draw additional cards */
        combat.QueueImmediate(
        [
            new ADrawCard
            {
                count = 1
            }
        ]
        );
        /* Draw additional cards */
        /* Patch RenderCharacters */
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Character), nameof(Character.RenderCharacters)),
            prefix: new HarmonyMethod(GetType(), nameof(Character_RenderCharacters_Prefix))
        );
        /* Patch RenderCharacters */
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
        }
        /* Add Crew Member */
    }
    public override void OnCombatStart(State state, Combat combat)
    {
    }
    public static bool Character_RenderCharacters_Prefix(Character __instance, G g, Vec offset, bool mini, double introTimer, bool showDialogue, ref bool finaleMode)
	{
		Rect? rect = default(Rect) + offset;
		g.Push(null, rect, null, autoFocus: false, noHoverSound: false, gamepadUntargetable: false, ReticleMode.Quad, null, null, null, null, 0, null, null, null, null);
		int num = 0;
		int num2 = 0;
        int characterCount = 0;
		for (int i = 0; i < g.state.characters.Count; i++)
		{
            characterCount++;
			Character character = g.state.characters[i];
			int x = num2 + (int)Mutil.AnimHelper(introTimer, -90.0, 0.0, 360.0, 0.0 + (double)i * 0.1);
            int y = num;
			bool mini2 = mini;
			bool isExploding = g.state.ship.hull <= 0;
			bool isDoneExploding = g.state.ship.deathProgress >= 1.0;
			bool hasValue = g.state.ship.escapeTimer.HasValue;
			bool showDialogue2 = showDialogue;
#pragma warning disable DirectGameEnumUsage // Avoid direct game enum usage
            UIKey? upHint = ((i == 0) ? new UIKey?(UK.corner_mainmenu) : ((UIKey?)null));
#pragma warning restore DirectGameEnumUsage // Avoid direct game enum usage
            double blurbAlign = -1.0;
			character.Render(g, x, y, flipX: false, mini2, isExploding, isDoneExploding, hasValue, null, renderLocked: false, hideFace: false, showUnlockInstructions: false, showDialogue2, null, null, null, upHint, null, canFocus: true, null, autoFocus: false, showTooltips: true, i switch
			{
				0 => -10, 
				1 => -20, 
				2 => -30, 
				3 => -10,
                4 => -20,
                5 => -30,
                _ => -10
			}, blurbAlign);
            if (finaleMode || g.state.characters.Count > 3)
			{
                if (characterCount == 0)
                {
                    num += 32;
                }
                if (characterCount < 3)
                {
                    num += 32;
                }
                else
                {
                    num = 0;
                    num2 += 34;
                    characterCount = 0;
                }
				mini = true;
			}
			else
			{
				num += mini ? 32 : 60;
			}
		}
		g.Pop();
        return false;
	}
}