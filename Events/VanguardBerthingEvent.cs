using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;

namespace Vionheart.Events;

internal sealed class VanguardBerthingEvent : IRegisterable
{
    private static string EventName = null!;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        EventName = $"{package.Manifest.UniqueName}::{MethodBase.GetCurrentMethod()!.DeclaringType!.Name}";
        DB.story.all[EventName] = new()
        {
            type = NodeType.@event,
            lines = [
                new CustomSay
                {
                    who = "comp",
                    loopTag = "neutral",
                    Text = "Who ya gonna call?"
                }
            ]
        };
        DB.eventChoiceFns[EventName] = AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(GetChoices));
    }
    private static List<Choice> GetChoices(State state)
    {
        Rand rng = new Rand(state.rngCurrentEvent.seed + 3629);
        Ship ship = state.ship;
        List<Deck> list = (from dt in state.storyVars.GetUnlockedChars()
        where !state.characters.Any((Character ch) => ch.deckType == (Deck?)dt)
        select dt).ToList();

        List<Choice> choice = new List<Choice>();
        if (list.Count > 0)
        {
            Deck foundCharacter = list.Random(rng);
            string key = "VanguardBerthingEvent_" + foundCharacter.Key();
            if (!DB.story.all.ContainsKey(key))
            {
                key = "VanguardBerthingEvent_colorless";
            }
            choice.AddRange(state.characters.Select((Character ch) => new Choice
            {
                label = "VANGUARD BERTHING CHOICE",
                key = key,
                actions = {
                    (CardAction) new AAddCharacter
                    {
                        deck = foundCharacter,
                        addTheirStarterCardsAndArtifacts = true,
                        canGoPastTheCharacterLimit = true
                    }
                }
            }
            )
            );
        }
        return choice;
    }
}