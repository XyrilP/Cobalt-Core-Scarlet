using System.Collections.Generic;
using System.Security.Cryptography;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal class CombatDialogueV2 : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {
                "Scarlet_FadeTutorial_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Fade ],
                    once = true,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "My <c=status>FADE</c> can <c=keyword>dodge</c> through <c=keyword>attacks, missiles and enemy intents</c>!")
                    ]
                }
            },
            {
                "Scarlet_SaturationBarrageTutorial_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ SaturationBarrage ],
                    once = true,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "With <c=status>SATURATION BARRAGE</c>, I can <c=keyword>make extra attacks</c> for <c=downside>half damage</c>!")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Golly!")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Strike!")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Attack.")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_3",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Oh, how I strike.")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_4",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Defend yourself.")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_5",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "See here!")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_6",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "smug", "I see you.")
                    ]
                }
            },
            {
                "Scarlet_JustHitGeneric_7",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "smug", "Heh heh heh he.")
                    ]
                }
            },
            {
                "Scarlet_Riggs_JustHitGeneric_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Riggs ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Scarlet, "happy", "Score! Ya saw that, Riggs?"),
                        new(Riggs, "neutral", "Mmmyeah!")
                    ]
                }
            },
            {
                "Scarlet_Riggs_JustHitGeneric_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Riggs ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    dialogue =
                    [
                        new(Riggs, "neutral", "Ooooh, that one is a good hit!"),
                        new(Scarlet, "happy", "Good girl."),
                    ]
                }
            },
            {
                "Scarlet_Riggs_JustHitGeneric_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Riggs ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 2,
                    dialogue =
                    [
                        new(Scarlet, "dagger", "You don't stand a chance."),
                        new(Riggs, "gun", "Not in a gazillion years!")
                    ]
                }
            }
        }
        );
    }
}