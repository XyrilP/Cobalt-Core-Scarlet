using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VionheartScarlet;
using Microsoft.Extensions.Logging;
using Nickel;
using VionheartScarlet.Artifacts;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal static class ArtifactDialogue
{
    internal static VionheartScarlet Instance => VionheartScarlet.Instance;
    internal static string F(this string Name)
    {
        return $"{Instance.UniqueName}::{Name}";
    }
    internal static void Inject()
    {
        DB.story.all["Scarlet_Artifact_CloakAndDagger_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"CloakAndDagger".F() ],
            oncePerRunTags = [ "CloakAndDaggerTag" ],
            turnStart = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Cloak engaged!",
                    loopTag = "neutral" // Requires stealthy
                }
            }
        };

        DB.story.all["Scarlet_Artifact_CloakAndDagger_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"CloakAndDagger".F() ],
            playerJustPiercedEnemyArmor = true,
            lookup = [ "CloakAndDaggerTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "A dagger in your backs!",
                    loopTag = "neutral" // Requires stealthy
                }
            }
        };

        DB.story.all["Scarlet_Artifact_HardlightAfterburners_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"HardlightAfterburners".F() ],
            oncePerRunTags = [ "HardlightAfterburnersTag" ],
            maxTurnsThisCombat = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Hardlight Afterburners on stand by.",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_HardlightAfterburners_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"HardlightAfterburners".F() ],
            lookup = [ "HardlightAfterburnersTrigger" ],
            turnStart = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Hardlight Afterburners engaged!",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ElectrolyteSurge_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ElectrolyteSurge".F() ],
            lookup = [ "ElectrolyteSurgeTrigger" ],
            oncePerCombat = true,
            priority = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Nothin' like good ol' soda pop!",
                    loopTag = "neutral" //requires drinking emote
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ElectrolyteSurge_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ElectrolyteSurge".F() ],
            lookup = [ "ElectrolyteSurgeTrigger" ],
            oncePerCombat = true,
            priority = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "I'll drink to that.",
                    loopTag = "neutral" //requires drinking emote
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ElectrolyteSurge_Multi_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ElectrolyteSurge".F() ],
            lookup = [ "ElectrolyteSurgeTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Again!",
                    loopTag = "neutral" //requires drinking emote
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ElectrolyteSurge_Multi_3"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ElectrolyteSurge".F() ],
            lookup = [ "ElectrolyteSurgeTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "And again!",
                    loopTag = "neutral" //requires drinking emote
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ElectrolyteSurge_Multi_4"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ElectrolyteSurge".F() ],
            lookup = [ "ElectrolyteSurgeTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "... Again!",
                    loopTag = "neutral" //requires drinking emote
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ReactionWheel_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ReactionWheel".F() ],
            oncePerRunTags = [ "ReactionWheelTag" ],
            turnStart = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Reaction Wheel active, let's roll.",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ReactionWheel_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ReactionWheel".F() ],
            lookup = [ "ReactionWheelTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Rollin' and rollin'!",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_ReactionWheel_Multi_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"ReactionWheel".F() ],
            lookup = [ "ReactionWheelTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Keep it rollin'!",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_TrickAction_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"TrickAction".F() ],
            oncePerRunTags = [ "TrickActionTag" ],
            turnStart = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Just remembered a few tricks, let's see it in action.",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_TrickAction_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"TrickAction".F() ],
            lookup = [ "TrickActionTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Trick!",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_TrickAction_Multi_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"TrickAction".F() ],
            lookup = [ "TrickActionTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Peace out!",
                    loopTag = "neutral"
                }
            }
        };

        DB.story.all["Scarlet_Artifact_TrickAction_Multi_3"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"TrickAction".F() ],
            lookup = [ "TrickActionTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Phasing!",
                    loopTag = "neutral"
                }
            }
        };
    }
}