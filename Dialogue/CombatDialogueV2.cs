using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet;
using VionheartScarlet.Artifacts;
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
                    whoDidThat = Scarlet_Deck,
                    priority = true,
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
                    whoDidThat = Scarlet_Deck,
                    priority = true,
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
                        new(Scarlet, "smug", "Oh, how I strike.")
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
                        new(Scarlet, "smug", "Defend yourself.")
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
                        new(Scarlet, "lockedin", "See here!")
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
                    whoDidThat = Scarlet_Deck,
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
                    minDamageDealtToEnemyThisAction = 3,
                    whoDidThat = Riggs_Deck,
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
                    minDamageDealtToEnemyThisAction = 3,
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "dagger", "You don't stand a chance."),
                        new(Riggs, "gun", "Not in a gazillion years!")
                    ]
                }
            },
            {
                "Scarlet_Weth_JustHitGeneric_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Riggs ],
                    playerShotJustHit = true,
                    minDamageDealtToEnemyThisAction = 1,
                    whoDidThat = Weth_Deck,
                    dialogue =
                    [
                        new(Weth, "neutral", "That's a hit!"),
                        new(
                        [
                            new(Scarlet, "happy", "Good girl."),
                            new(Scarlet, "smug", "Outstanding.")
                        ]
                        )
                    ]
                }
            },
            {
                "Scarlet_MovedALittle_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 1,
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Step!")
                    ]
                }
            },
            {
                "Scarlet_MovedALittle_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 1,
                    dialogue =
                    [
                        new(Scarlet, "squint", "A little bit to the side here...")
                    ]
                }
            },
            {
                "Scarlet_MovedALittle_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 1,
                    dialogue =
                    [
                        new(Scarlet, "smug", "Since you put it like that.")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Stride!")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Y'all might feel a lil' turbulence!")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "locked", "Comin' through!")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_3",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Quickly.")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_4",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Quickly, quickly.")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_5",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "I sprint.")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_6",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Dispatched.")
                    ]
                }
            },
            {
                "Scarlet_WeAreMovingAroundAlot_7",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Post haste.")
                    ]
                }
            },
            {
                "Scarlet_Riggs_WeAreMovingAroundAlot_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Riggs ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Let's turn here, Riggs!"),
                        new(Riggs, "neutral", "Okay!")
                    ]
                }
            },
            {
                "Scarlet_Riggs_WeAreMovingAroundAlot_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Riggs ],
                    minMovesThisTurn = 3,
                    dialogue =
                    [
                        new(Riggs, "neutral", "Over there, Scarlet!"),
                        new(Scarlet, "lockedin", "Roger!")
                    ]
                }
            },
            {
                "Scarlet_Fade_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Fade ],
                    oncePerCombatTags = [ "Scarlet_Fade" ],
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Fading.")
                    ]
                }
            },
            {
                "Scarlet_Fade_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Fade ],
                    oncePerCombatTags = [ "Scarlet_Fade" ],
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Watch this Fade.")
                    ]
                }
            },
            {
                "Scarlet_Fade_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Fade ],
                    oncePerCombatTags = [ "Scarlet_Fade" ],
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "They can't hit us.")
                    ]
                }
            },
            {
                "Scarlet_Fade_3",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Fade ],
                    oncePerCombatTags = [ "Scarlet_Fade" ],
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "We are concealed.")
                    ]
                }
            },
            {
                "Scarlet_Fade_4",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Fade ],
                    oncePerCombatTags = [ "Scarlet_Fade" ],
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "We are hidden.")
                    ]
                }
            },
            {
                "Scarlet_SaturationBarrage_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ SaturationBarrage ],
                    oncePerCombatTags = [ "Scarlet_SaturationBarrage" ],
                    whoDidThat = Scarlet_Deck,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Barrage ready!"),
                        new(
                        [
                            new(Riggs, "neutral", "Get 'em! Get 'em!"),
                            new(Hyperia, "vengeful", "Unload on them!"),
                            new(Eunice, "sly", "Atta boy!"),
                            new(Weth, "lockedin", "Yes! Yes! Yes!")
                        ]
                        )
                    ]
                }
            },
            {
                "Scarlet_StrafeShot_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToEnemyThisAction = 1,
                    playerShotWasFromStrafe = true,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Strafing!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "No!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "You'll pay for that!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "THIS IS WHAT HAPPENS WHEN YOU MAKE A MISTAKE.")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_3",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "No! Not the ship! Not the ship!!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_4",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "No! We took a hit!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_5",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "Watch the paint!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_6",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "tired", "What are you doin'?")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_7",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "tired", "Again with the ship?")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_8",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "What? Again?!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_9",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "tired", "Everytime...")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_10",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "I sense I've made a mistake of some kind...")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_11",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "Aw man!")
                    ]
                }
            },
            {
                "Scarlet_Ruhig_ShipTookDamage_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Ruhig ],
                    whoDidThat = Ruhig_Deck,
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "RUHIG!!!"),
                        new(
                        [
                            new(Ruhig, "neutral", "Uh oh."),
                            new(Ruhig, "neutral", "I'm not in trouble, am I?")
                        ]
                        )
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_Vanguard_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    hasArtifacts = [ $"VanguardBerthing".F() ],
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "What is it with everybody wreckin' my ship?!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_Vanguard_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    hasArtifacts = [ $"VanguardBerthing".F() ],
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "Not my ride!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_PerfectRuined_Combat_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    priority = true,
                    dialogue =
                    [

                        new(Scarlet, "angry", "Welp, there goes trying not to get hit in this fight!")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_PerfectRuined_Combat_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    priority = true,
                    dialogue =
                    [
                        new(Scarlet, "squint", "Don't be sorry. Be better.")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_PerfectRuined_Combat_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerCombatTags = [ "Scarlet_ShipTookDamage" ],
                    priority = true,
                    dialogue =
                    [
                        new(Scarlet, "tired", "Next time, I'll make the right choices.")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_PerfectRuined_Run_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerRunTags = [ "Scarlet_ShipTookDamage" ],
                    priority = true,
                    dialogue =
                    [
                        new(Scarlet, "squint", "So much for trying to not get hit.")
                    ]
                }
            },
            {
                "Scarlet_ShipTookDamage_PerfectRuined_Run_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    minDamageDealtToPlayerThisTurn = 1,
                    oncePerRunTags = [ "Scarlet_ShipTookDamage" ],
                    priority = true,
                    dialogue =
                    [
                        new(Scarlet, "tired", "Maybe we can do better next loop.")
                    ]
                }
            },
            {
                "Scarlet_Heat_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Status.heat ],
                    oncePerCombatTags = [ "Scarlet_Heat" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "Temps in bulkhead are goin' up.")
                    ]
                }
            },
            {
                "Scarlet_Heat_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Status.heat ],
                    oncePerCombatTags = [ "Scarlet_Heat" ],
                    dialogue =
                    [
                        new(Scarlet, "tired", "Heesh, finally broke a sweat.")
                    ]
                }
            },
            {
                "Scarlet_Heat_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Status.heat ],
                    oncePerCombatTags = [ "Scarlet_Heat" ],
                    dialogue =
                    [
                        new(Scarlet, "nervous", "Am I sweating?")
                    ]
                }
            },
            {
                "Scarlet_Overheat_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    oncePerCombatTags = [ "OverheatGeneric" ],
                    goingToOverheat = true,
                    dialogue =
                    [
                        new(Scarlet, "scream", "Hot! Hot! Hot! Hot!")
                    ]
                }
            },
            {
                "Scarlet_Overheat_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    oncePerCombatTags = [ "OverheatGeneric" ],
                    goingToOverheat = true,
                    dialogue =
                    [
                        new(Scarlet, "scream", "I'm gettin' cooked here!")
                    ]
                }
            },
            {
                "Scarlet_Overheat_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    oncePerCombatTags = [ "OverheatGeneric" ],
                    goingToOverheat = true,
                    dialogue =
                    [
                        new(Scarlet, "scream", "Feelin' like in the fryer right now!")
                    ]
                }
            },
            {
                "Scarlet_Overheat_3",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    oncePerCombatTags = [ "OverheatGeneric" ],
                    goingToOverheat = true,
                    dialogue =
                    [
                        new(Scarlet, "scream", "I'm gettin' nuked 'ere!")
                    ]
                }
            },
            {
                "Scarlet_Drake_Overheat_DrakesFault_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Eunice ],
                    oncePerCombatTags = [ "OverheatDrakesFault" ],
                    goingToOverheat = true,
                    whoDidThat = Eunice_Deck,
                    dialogue =
                    [
                        new(Scarlet, "scream", "Eunice! Cool it down, will ya?!"),
                        new(Eunice, "mad", "Ugh, just open a window or something.")
                    ]
                }
            },
            {
                "Scarlet_Drake_Overheat_DrakesFault_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Eunice ],
                    oncePerCombatTags = [ "OverheatDrakesFault" ],
                    goingToOverheat = true,
                    whoDidThat = Eunice_Deck,
                    dialogue =
                    [
                        new(Eunice, "sly", "Is it just me or-"),
                        new(Scarlet, "scream", "Cool it down, Eunice!")
                    ]
                }
            },
            {
                "Scarlet_Corrosion_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet ],
                    lastTurnPlayerStatuses = [ Status.corrode ],
                    oncePerCombatTags = [ "Scarlet_Corrosion" ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "Is it just me or is the cockpit melting?")
                    ]
                }
            },
            {
                "Scarlet_Illeana_Corrosion_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Illeana ],
                    whoDidThat = Illeana_Deck,
                    lastTurnPlayerStatuses = [ Status.corrode ],
                    oncePerCombatTags = [ "Scarlet_Illeana_Corrosion" ],
                    dialogue =
                    [
                        new(Scarlet, "angry", "ILLEANA!!!"),
                        new(
                        [
                            new(Illeana, "silly", "Don't worry about it!"),
                            new(Illeana, "explain", "We'll end up with a net positive, eventually.")
                        ]
                        )
                    ]
                }
            },
            {
                "Scarlet_AboutToDie_0",
                new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = [ "aboutToDie" ],
                    allPresent = [ Scarlet ],
                    oncePerRun = true,
                    enemyShotJustHit = true,
                    maxHull = 1,
                    dialogue =
                    [
                        new(Scarlet, "tired", "Not yet... It's not over yet!")
                    ]
                }
            },
            {
                "Scarlet_AboutToDie_1",
                new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = [ "aboutToDie" ],
                    allPresent = [ Scarlet ],
                    oncePerRun = true,
                    enemyShotJustHit = true,
                    maxHull = 1,
                    dialogue =
                    [
                        new(Scarlet, "tired", "Is that all...?")
                    ]
                }
            },
            {
                "Scarlet_Riggs_AboutToDie_0",
                new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = [ "aboutToDie" ],
                    allPresent = [ Scarlet, Riggs ],
                    oncePerRun = true,
                    enemyShotJustHit = true,
                    maxHull = 1,
                    dialogue =
                    [
                        new(Scarlet, "lockedin", "Not yet, Riggs! It's not over yet!"),
                        new(Riggs, "sad", "Okay!")
                    ]
                }
            },
            {
                "Scarlet_Riggs_AboutToDie_1",
                new()
                {
                    type = NodeType.combat,
                    oncePerCombatTags = [ "aboutToDie" ],
                    allPresent = [ Scarlet, Riggs ],
                    oncePerRun = true,
                    enemyShotJustHit = true,
                    maxHull = 1,
                    dialogue =
                    [
                        new(Riggs, "serious", "It's okay Scarlet, this happens all the time."),
                        new(Scarlet, "tired", "Right...")
                    ]
                }
            },
            {
                "Scarlet_Ruhig_Drake_Present_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Ruhig, Eunice ],
                    oncePerRun = true,
                    dialogue =
                    [
                        new(Scarlet, "squint", "You two dragons. Behave."),
                        new(Ruhig, "neutral", "Aye, aye."),
                        new(Eunice, "sly", "Of course.")
                    ]
                }
            },
            {
                "Scarlet_Illeana_Present_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Illeana ],
                    oncePerRunTags = [ "Scarlet_Illeana_Present" ],
                    dialogue =
                    [
                        new(Scarlet, "tired", "Illeana..."),
                        new(Illeana, "silly", "Hi Scarlet, it's me again!")
                    ]
                }
            },
            {
                "Scarlet_Illeana_Present_Vanguard_0",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Illeana ],
                    oncePerRunTags = [ "Scarlet_Illeana_Present" ],
                    hasArtifacts = [ $"VanguardBerthing".F() ],
                    dialogue =
                    [
                        new(Scarlet, "squint", "No."),
                        new(Illeana, "explains", "I haven't even asked yet.")
                    ]
                }
            },
            {
                "Scarlet_Illeana_Present_Vanguard_1",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Illeana ],
                    oncePerRunTags = [ "Scarlet_Illeana_Present" ],
                    hasArtifacts = [ $"VanguardBerthing".F() ],
                    dialogue =
                    [
                        new(Illeana, "salavating", "Oh my."),
                        new(Scarlet, "angry", "Oh no you will not mess up my ship!")
                    ]
                }
            },
            {
                "Scarlet_Illeana_Present_Vanguard_2",
                new()
                {
                    type = NodeType.combat,
                    allPresent = [ Scarlet, Illeana ],
                    oncePerRunTags = [ "Scarlet_Illeana_Present" ],
                    hasArtifacts = [ $"VanguardBerthing".F() ],
                    dialogue =
                    [
                        new(Illeana, "giggle", "Scarlet can I? Scarlet can I? Scarlet can I?"),
                        new(Scarlet, "tired", "Sigh.")
                    ]
                }
            }
        }
        );
    }
}