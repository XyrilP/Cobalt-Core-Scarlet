using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VionheartScarlet;
using Microsoft.Extensions.Logging;
using Nickel;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal static class CombatDialogue
{
    internal static void Inject()
    {
        Replies();
        ModdedInject();
        MainExtensions();
        ScarletCombat();
    }

    private static void MainExtensions()
    {
        DB.story.all["Scarlet_JustHitGeneric_0"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Golly!",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};
		
        DB.story.all["Scarlet_JustHitGeneric_1"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Strike!",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};

		DB.story.all["Scarlet_JustHitGeneric_2"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Attack.",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};

		DB.story.all["Scarlet_JustHitGeneric_3"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Oh, how I strike.",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};

		DB.story.all["Scarlet_JustHitGeneric_4"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Defend yourself.",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};

		DB.story.all["Scarlet_JustHitGeneric_5"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "See here!",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};

		DB.story.all["Scarlet_JustHitGeneric_6"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "I see you.",
				    loopTag = "neutral" // Requires "aggressive" emote.
			    }
            }
		};

		DB.story.all["Scarlet_JustHitGeneric_7"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Heh heh heh he.",
				    loopTag = "neutral" // Requires "smug" emote.
			    }
            }
		};

        DB.story.all["Scarlet_Riggs_JustHitGeneric_0"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			allPresent = [ Scarlet, Riggs ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Score! ya saw that, Riggs?",
				    loopTag = "neutral"
			    },
                new CustomSay
			    {
				    who = Riggs,
				    Text = "Mmm, yea~",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_MovedALittle_Multi_0"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Step!",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_MovedALittle_Multi_1"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "A little bit to the side here...",
				    loopTag = "squint"
			    }
            }
		};

		DB.story.all["Scarlet_MovedALittle_Multi_2"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 1,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Since you put it like that.",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_0"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Stride!",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_1"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Y'all might feel a little turbulence!",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_2"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Comin' through!",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_3"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Quickly.",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_4"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Quickly, quickly.",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_5"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "I sprint.",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_6"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Dispatched.",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_WeAreMovingAroundAlot_Multi_7"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Post haste.",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_Riggs_WeAreMovingAroundAlot_Multi_0"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
			allPresent = [ Scarlet, Riggs ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Let's turn here, Riggs!",
				    loopTag = "neutral"
			    },
				new CustomSay
				{
					who = Riggs,
					Text = "Okay!",
					loopTag = "neutral"
				}
            }
		};

		DB.story.all["Scarlet_Riggs_WeAreMovingAroundAlot_Multi_1"] = new()
		{
			type = NodeType.combat,
            minMovesThisTurn = 3,
			allPresent = [ Scarlet, Riggs ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Riggs,
				    Text = "Over there, Scarlet!",
				    loopTag = "neutral"
			    },
				new CustomSay
				{
					who = Scarlet,
					Text = "Roger!",
					loopTag = "neutral"
				}
            }
		};

        DB.story.all["Scarlet_FadeGeneric_0"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [VionheartScarlet.Instance.Fade.Status],
			oncePerCombatTags = [ "FadeGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Fading.",
				    loopTag = "neutral" // Requires "stealthy" emote
			    }
            }
		};

        DB.story.all["Scarlet_FadeGeneric_1"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [VionheartScarlet.Instance.Fade.Status],
			oncePerCombatTags = [ "FadeGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Watch this fade.",
				    loopTag = "neutral" // Requires "stealthy" emote
			    }
            }
		};

        DB.story.all["Scarlet_FadeGeneric_2"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [VionheartScarlet.Instance.Fade.Status],
			oncePerCombatTags = [ "FadeGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "They can't see us.",
				    loopTag = "neutral" // Requires "stealth" emote
			    }
            }
		};

		DB.story.all["Scarlet_FadeGeneric_3"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [VionheartScarlet.Instance.Fade.Status],
			oncePerCombatTags = [ "FadeGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "We are concealed.",
				    loopTag = "neutral" // Requires "stealth" emote
			    }
            }
		};

		DB.story.all["Scarlet_FadeGeneric_4"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [VionheartScarlet.Instance.Fade.Status],
			oncePerCombatTags = [ "FadeGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "We are hidden.",
				    loopTag = "neutral" // Requires "stealth" emote
			    }
            }
		};

		DB.story.all["Scarlet_TemporaryStrafeGeneric_0"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [ VionheartScarlet.Instance.SaturationBarrage.Status ],
			oncePerCombatTags = [ "TemporaryStrafeTag" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Run and Gun!",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_TemporaryStrafeGeneric_1"] = new()
		{
			type = NodeType.combat,
            lastTurnPlayerStatuses = [ VionheartScarlet.Instance.SaturationBarrage.Status ],
			oncePerCombatTags = [ "TemporaryStrafeTag" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Lemme do a snap shot!",
				    loopTag = "neutral"
			    }
            }
		};

		DB.story.all["Scarlet_StrafeShot_0"] = new()
		{
			type = NodeType.combat,
			playerShotJustHit = true,
			minDamageDealtToEnemyThisAction = 1,
			playerShotWasFromStrafe = true,
			oncePerCombat = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Strafing!",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_OverheatGeneric_0"] = new()
		{
			type = NodeType.combat,
			goingToOverheat = true,
			oncePerCombatTags = [ "OverheatGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Temperature in the bulkhead are beyond safe thresholds!",
				    loopTag = "squint" // Requires "worried" emote
			    }
            }
		};

        DB.story.all["Scarlet_OverheatGeneric_1"] = new()
		{
			type = NodeType.combat,
			goingToOverheat = true,
			oncePerCombatTags = [ "OverheatGeneric" ],
			allPresent = [ Scarlet ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Hot, hot, hot!",
				    loopTag = "squint" // Requires "worried" emote
			    }
            }
		};

        DB.story.all["Scarlet_Eunice_OverheadDrakesFault_Multi_0"] = new()
		{
			type = NodeType.combat,
			goingToOverheat = true,
            whoDidThat = Eunice_Deck,
			oncePerCombatTags = [ "OverheatDrakesFault" ],
			allPresent = [ Scarlet, Eunice ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Eunice! Cool it down, will ya?!",
				    loopTag = "neutral" // Requires "worried" emote.
			    },
                new CustomSay
                {
                    who = Eunice,
                    Text = "Ugh, just open a window or something.",
                    loopTag = "mad"
                }
            }
		};

        DB.story.all["Scarlet_Eunice_OverheadDrakesFault_Multi_1"] = new()
		{
			type = NodeType.combat,
			goingToOverheat = true,
            whoDidThat = Eunice_Deck,
			oncePerCombatTags = [ "OverheatDrakesFault" ],
			allPresent = [ Scarlet, Eunice ],
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Drake! chill, chill!!",
				    loopTag = "neutral" // Requires "worried" emote.
			    }
            }
		};

        DB.story.all["Scarlet_OneHitPointThisIsFine_Multi_0"] = new()
		{
			type = NodeType.combat,
			oncePerCombatTags = [ "aboutToDie" ],
			allPresent = [ Scarlet ],
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Riggs..!",
				    loopTag = "neutral"
			    }
            }
		};

        DB.story.all["Scarlet_Riggs_OneHitPointThisIsFine_Multi_0"] = new()
		{
			type = NodeType.combat,
			oncePerCombatTags = [ "aboutToDie" ],
			allPresent = [ Scarlet, Riggs ],
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
			lines = new()
            {
                new CustomSay
			    {
				    who = Scarlet,
				    Text = "Riggs, hold my hand!",
				    loopTag = "neutral"
			    },
                new CustomSay
                {
                    who = Riggs,
                    Text = "Okay!",
                    loopTag = "sad"
                }
            }
		};

        DB.story.all["Scarlet_Riggs_OneHitPointThisIsFine_Multi_1"] = new()
		{
			type = NodeType.combat,
			oncePerCombatTags = [ "aboutToDie" ],
			allPresent = [ Scarlet, Riggs ],
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
			lines = new()
            {
                new CustomSay
			    {
				    who = Riggs,
				    Text = "Hey, Scarlet. Everything's going to be fine.",
				    loopTag = "serious"
			    },
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Y-Yeah..",
                    loopTag = "squint"
                }
            }
		};

    }

    private static void Replies()
    {

    }

    private static void ScarletCombat()
    {

    }

    private static void ModdedInject()
    {

    }
}