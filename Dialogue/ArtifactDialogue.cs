using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using XyrilP.VionheartScarlet;
using Microsoft.Extensions.Logging;
using Nickel;
using XyrilP.VionheartScarlet.Artifacts;

namespace XyrilP.VionheartScarlet.Dialogue;

internal static class ArtifactDialogue
{
    internal static VionheartScarlet Instance => VionheartScarlet.Instance;
    internal static string F(this string Name)
    {
        return $"{Instance.UniqueName}::{Name}";
    }
    internal static void Inject()
    {
        var Scarlet = VionheartScarlet.Instance.Scarlet_Deck.UniqueName;

        DB.story.all["Scarlet_Artifact_CloakAndDagger_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ Scarlet ],
            hasArtifacts = [ $"CloakAndDagger".F() ],
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
            minDamageDealtToEnemyThisAction = 1,
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
            oncePerCombatTags = [ "HardlightAfterburnersTag" ],
            turnStart = true,
            lines = new()
            {
                new CustomSay
                {
                    who = Scarlet,
                    Text = "Poppin' the afterburners!",
                    loopTag = "neutral"
                }
            }
        };
    }
}