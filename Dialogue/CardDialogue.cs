using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vionheart;
using Microsoft.Extensions.Logging;
using Nickel;
using static Vionheart.Dialogue.CommonDefinitions;

namespace Vionheart.Dialogue;

internal static class CardDialogue
{
    internal static void Inject()
    {
        DB.story.all["Scarlet_SneakAttack_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletSneakAttack" ],
            oncePerCombatTags = [ "scarletSneakAttackTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Sneaking one in through their defenses.",
                    loopTag = "neutral" // Requires "aggressive" emote
                }
            }
        };

        DB.story.all["Scarlet_SneakAttack_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletSneakAttack" ],
            oncePerCombatTags = [ "scarletSneakAttackTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "SNEAK ATTACK!!!",
                    loopTag = "neutral" // Requires "aggressive" emote
                }
            }
        };

        DB.story.all["Scarlet_SneakAttack_Multi_2"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletSneakAttack" ],
            oncePerCombatTags = [ "scarletSneakAttackTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Peek-aboo.",
                    loopTag = "neutral" // Requires "aggressive" emote
                }
            }
        };

        DB.story.all["Scarlet_HideAndSneak_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletHideAndSneak" ],
            oncePerCombatTags = [ "scarletHideAndSneakTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "I go in silence.",
                    loopTag = "neutral" // Requires "stealthy" emote
                }
            }
        };

        DB.story.all["Scarlet_HideAndSneak_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletHideAndSneak" ],
            oncePerCombatTags = [ "scarletHideAndSneakTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Quietly now.",
                    loopTag = "neutral" // Requires "stealthy" emote
                }
            }
        };

        DB.story.all["Scarlet_HideAndSneak_Multi_2"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletHideAndSneak" ],
            oncePerCombatTags = [ "scarletHideAndSneakTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Like a whisper.",
                    loopTag = "neutral" // Requires "stealthy" emote
                }
            }
        };

        DB.story.all["Scarlet_HideAndSneak_Multi_3"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletHideAndSneak" ],
            oncePerCombatTags = [ "scarletHideAndSneakTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Silent as smoke.",
                    loopTag = "neutral" // Requires "stealthy" emote
                }
            }
        };

        DB.story.all["Scarlet_HideAndSneak_Multi_4"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletHideAndSneak" ],
            oncePerCombatTags = [ "scarletHideAndSneakTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Quiet as cat's feet.",
                    loopTag = "neutral" // Requires "stealthy" emote
                }
            }
        };

        DB.story.all["Scarlet_BlinkStrike_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletBlinkStrike" ],
            oncePerCombatTags = [ "scarletBlinkStrikeTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Blink and you'll miss it!",
                    loopTag = "neutral" // Requires "aggressive" emote
                }
            }
        };

        DB.story.all["Scarlet_TricksOfTheTrade_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletTricksOfTheTrade" ],
            oncePerCombatTags = [ "scarletTricksOfTheTradeTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Wanna see something cool?",
                    loopTag = "neutral" // Requires "tricks" emote
                }
            }
        };

        DB.story.all["Scarlet_FadeAway_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Scarlet ],
            lookup = [ "scarletFadeAway" ],
            oncePerCombatTags = [ "scarletFadeAwayTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "I slide between sunbeams.",
                    loopTag = "neutral" // Requires "stealthy" emote
                }
            }
        };

        DB.story.all["Cat_SummonScarlet_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Cat ],
            lookup = [ "summonScarlet" ],
            oncePerCombatTags = [ "summonScarletTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Cat,
                    Text = "Maybe Scarlet has a trick for this.",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Cat_SummonScarlet_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Cat ],
            lookup = [ "summonScarlet" ],
            oncePerCombatTags = [ "summonScarletTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Cat,
                    Text = "Death comes for you.",
                    loopTag = "grumpy"
                },
                new CustomSay
                {
                    who = Cat,
                    Text = "How's my Scarlet impression?",
                    loopTag = "peace"
                }
            }
        };

        DB.story.all["Cat_Scarlet_SummonScarlet_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Cat, Scarlet ],
            lookup = [ "summonScarlet" ],
            oncePerCombatTags = [ "summonScarletTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Cat,
                    Text = "Let's trick this ship.",
                    loopTag = "smug"
                },
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Can you keep up?",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Cat_Scarlet_Riggs_SummonScarlet_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerCombat = true,
            allPresent = [ Cat, Scarlet, Riggs ],
            lookup = [ "summonScarlet" ],
            oncePerCombatTags = [ "summonScarletTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Cat,
                    Text = "Let me take the wheel.",
                    loopTag = "lean"
                },
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Hey, watch it!",
                    loopTag = "neutral"
                },
                new CustomSay
                {
                    who = Riggs,
                    Text = "Gosh!",
                    loopTag = "nervous"
                },
                new CustomSay
                {
                    who = Riggs,
                    Text = "The controls are going crazy!",
                    loopTag = "neutral"
                }
            }
        };
    }
}