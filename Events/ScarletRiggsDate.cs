using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;
using static VionheartScarlet.Dialogue.CommonDefinitions;
using VionheartScarlet.Artifacts;

namespace VionheartScarlet.Events;

internal sealed class Scarlet_Riggs_Date : IRegisterable
{
    private static string EventName = null!;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        EventName = "Scarlet_Riggs_Date";
        DB.story.all[EventName] = new()
        {
            type = NodeType.@event,
            canSpawnOnMap = true,
            oncePerRun = true,
            allPresent = [ Scarlet, Riggs ],
            bg = "BGCafe",
            lines = [
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "Hey look, it's Brimford's!"
                },
                new CustomSay
                {
                    who = Brimford,
                    Text = "Hello.",
                    flipped = true
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "A cafe?"
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "Yeah! let's go buy a drink!"
                },
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Scarlet: "
                },
            ],
            choiceFunc = EventName
        };
        DB.eventChoiceFns[EventName] = AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(DateChoices));

        DB.story.all[EventName + "_NoFight"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            bg = "BGCafe",
            lines = [
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "That was fun!"
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "Yeah."
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "..."
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "Hey Riggs."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "bobaSlurp",
                    Text = "Yessssss?"
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "It's amazing to have you around."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "Aww! you too!"
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "I'll always be with you."
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "Don't ever doubt that I wouldn't."
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "No matter what you do."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "sad",
                    Text = "..."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "sad",
                    Text = "Thank you. Scarlet."
                }
            ]
        };

        DB.story.all[EventName + "_Fight"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            bg = "BGCafe",
            lines = [
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "That was fun!"
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "Yeah."
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "gameover",
                    Text = "?!"
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "We took too long!"
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "serious",
                    Text = "Gosh!"
                }
            ],
            choiceFunc = EventName + "_EnterCombat"
        };
        DB.eventChoiceFns[EventName + "_EnterCombat"] = AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(EnterCombatChoices));

        DB.story.all[EventName + "_TakeOut"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            bg = "BGCafe",
            lines = [
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "Take out is nice."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "sad",
                    Text = "Aww..."
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "neutral",
                    Text = "We're in a bit of a hurry..."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "Okay!"
                }
            ]
        };

        DB.story.all[EventName + "_NoDating"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            bg = "BGCafe",
            lines = [
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "sad",
                    Text = "..."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "neutral",
                    Text = "...?"
                },
                new CustomSay
                {
                    who = Scarlet,
                    loopTag = "sad",
                    Text = "Sorry, not now, Riggs."
                },
                new CustomSay
                {
                    who = Riggs,
                    loopTag = "sad",
                    Text = "W...What?"
                }
            ]
        };
    }
    private static List<Choice> DateChoices(State state)
    {
        return [
        new Choice
        {
            label = "\"Let's go on a date.\"",
            chance = state.GetHardEvents() ? 0.75 : 0.50,
            key = EventName + "_NoFight",
            actions = [
                new AAddArtifact
                {
                    artifact = new TwoPilots()
                },
                new AArtifactOffering
                {
                    amount = 1,
                    canSkip = true
                }
            ],
            keyChance = EventName + "_Fight",
            actionsChance = [
                new AArtifactOffering
                {
                    amount = 1,
                    canSkip = true
                },
                new ADanger
                {
                }
            ]
        },
        new Choice
        {
            label = "\"Take out is nice.\"",
            key = EventName + "_TakeOut",
            actions = [
                new AAddArtifact
                {
                    artifact = new CaffeineRush()
                },
                new AAddArtifact
                {
                    artifact = new ElectrolyteSurge()
                }
            ]
        },
        new Choice
        {
            label = "(I shouldn't do this!)",
            key = EventName + "_NoDating"
        }
    ];
    }
    private static List<Choice> EnterCombatChoices(State state)
    {
        return [
        new Choice
        {
            label = "Run to the ship!",
            key = EventName + "_EnterCombat",
            actions = [
                new AHurt
                {
                    hurtAmount = 1,
                    targetPlayer = true
                },
                new AStartCombat
                {
                    ai = new HeavyFighter()
                }
            ]
        }
    ];
    }
}