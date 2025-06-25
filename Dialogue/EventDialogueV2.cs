using System.Collections.Generic;
using System.Security.Cryptography;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal class EventDialogueV2 : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {
                "AbandonedShipyard_Repaired",
                new()
                {
                    edit =
                    [
                        new("7657a54e", Scarlet, "Look at that! I even gave it a little wash.")
                    ]
                }
            },
            {
                $"ChoiceCardRewardOfYourColorChoice_{Scarlet}",
                new()
                {
                    type = NodeType.@event,
                    oncePerRun = true,
                    bg = "BGBootSequence",
                    dialogue = [
                        new(Scarlet, "focus", "..."),
                        new(Triune, "Understood?"),
                        new(Scarlet, "lockedin", "Yes."),
                        new(Cat, "Energy readings are back to normal.")
                    ]
                }
            },
            {
                "CrystallizedFriendEvent",
                new()
                {
                    edit =
                    [
                        new("8383e940", Scarlet, "nervous", "If only we could keep everyone together.")
                    ]
                }
            },
            {
                $"CrystallizedFriendEvent_{Scarlet}",
                new()
                {
                    type = NodeType.@event,
                    oncePerRun = true,
                    bg = "BGCrystalizedFriend",
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                        new(new Wait{secs = 1.5}),
                        new(Scarlet, "tired", "Gah."),
                        new(
                        [
                            new(Scarlet, "lockedin", "Resurrect and try again."),
                            new(Scarlet, "lockedin", "Again."),
                            new(Scarlet, "lockedin", "One more time."),
                            new(Scarlet, "lockedin", "I'll get it right this time."),
                            new(Scarlet, "lockedin", "Once more, into the fray.")
                        ]
                        )
                    ]
                }
            },
            {
                "DraculaTime",
                new()
                {
                    edit =
                    [
                        new("077fc76a", Scarlet, "squint", "Wait... THE Dracula? like Count Vlad Dracula the Impaler?!")
                    ]
                }
            },
            {
                "EphemeralCardGift",
                new()
                {
                    edit =
                    [
                        new("db59f595", Scarlet, "smug", "This'll be useful.")
                    ]
                }
            },
            {
                "ForeignCardOffering_After",
                new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, Scarlet, "squint", "Wish they were here instead.")
                    ]
                }
            },
            {
                "ForeignCardOffering_Refuse",
                new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, Scarlet, "tired", "Apologies, but no.")
                    ]
                }
            },
            {
                "GrandmaShop",
                new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, Scarlet, "mischief", "Cream pies.")
                    ]
                }
            },
            {
                "Knight_1",
                new()
                {
                    edit =
                    [
                        new(EMod.countFromEnd, 1, Scarlet, "daggertaunt", "Come on.")
                    ]
                }
            },
            {
                "Knight_Midcombat_Greeting_Scarlet_Multi_0",
                new()
                {
                    type = NodeType.@event,
                    oncePerCombat = true,
                    allPresent = [ Scarlet, Ratzo ],
                    lookup = [ "knight_duel" ],
                    requiredScenes = [ "Knight_Midcombat_Greeting_1" ],
                    dialogue = [
                        new(Ratzo, "Ah, it is you! Let us partake in anothere <c=keyword>honorable duel</c>!", true),
                        new(Scarlet, "dagger", "Sir Ratzo, we meet again."),
                        new(Scarlet, "daggertaunt", "Shall our blades clash?"),
                        new(Ratzo, "Sayeth thy word, young roguish knave.", true)
                    ],
                    choiceFunc = "KnightCombatChoices"
                }
            },
            {
                "Knight_Midcombat_YouWin",
                new()
                {
                    dialogue = [
                        new(
                        [
                            new(Scarlet, "smug", "Didn't even break a sweat.")
                        ]
                        )
                    ]
                }
            },
            {
                "LoseCharacterCard",
                new()
                {
                    edit =
                    [
                        new("79d6356a", Scarlet, "scream", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!-")
                    ]
                }
            },
            {
                "LoseCharacterCard_No",
                new()
                {
                    edit =
                    [
                        new("ea6cfd2f", Scarlet, "sad", "THE SHIP!!!")
                    ]
                }
            },
            {
                $"LoseCharacterCard_{Scarlet}",
                new()
                {
                    type = NodeType.@event,
                    oncePerRun = true,
                    bg = "BGSupernova",
                    dialogue = [
                        new(Scarlet, "tired", "Anything but the ship...")
                    ]
                }
            },
            {
                "Sasha_2_Multi_2",
                new()
                {
                    edit =
                    [
                        new("89fa9389", Scarlet, "dagger", "Heheheheh.")
                    ]
                }
            },
            {
                "ShopkeeperInfinite_Scarlet_Multi_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ Scarlet ],
                    dialogue = [
                        new(Cleo, "Hello, Scarlet!", true),
                        new(Scarlet, "smug", "Cleo."),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },
            {
                "ShopkeeperInfinite_Scarlet_Multi_1",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ Scarlet ],
                    dialogue = [
                        new(Cleo, "Hey, handsome!", true),
                        new(Scarlet, "blush", "Hello, Four-eyes."),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },
            {
                "ShopkeeperInfinite_Scarlet_Riggs_Multi_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ Scarlet, Riggs ],
                    dialogue = [
                        new(Cleo, "Hey, handsome!", true),
                        new(Scarlet, "nervous", "Uh... Yeah."),
                        new(Riggs, "serious", "..."),
                        new(Riggs, "gun", "Hey."),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },
            {
                "SogginsEscape_1",
                new()
                {
                    edit =
                    [
                        new("ee7de136", Scarlet, "happy", "No worries.")
                    ]
                }
            },
            {
                "Soggins_1",
                new()
                {
                    edit =
                    [
                        new("ea76f8c1", Scarlet, "lockedin", "Come on, we have to help them!")
                    ]
                }
            },
            {
                "Soggins_Infinite",
                new()
                {
                    edit =
                    [
                        new("310495ff", Scarlet, "lockedin", "Don't panic! We're trying to help!")
                    ]
                }
            },
            {
                "TheCobalt_Scarlet_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = ["before_theCobalt"],
                    once = true,
                    priority = true,
                    requiredScenes = ["TheCobalt_2"],
                    allPresent = [ Scarlet ],
                    bg = "BGTheCobalt",
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Here we are, the Cobalt."),
                        new(Cat, "neutral", "You did well to get us here, Scarlet!"),
                        new(Scarlet, "neutral", "You're welcome. Now... Defeating the Cobalt will fix everything right?"),
                        new(Cat, "squint", "Something like that."),
                        new(Scarlet, "focus", "..."),
                        new(Scarlet, "lockedin", "To arms then."),
                        new(Cat, "transition", "Break a leg, pilot!"),
                        new(Scarlet, "lockedin", "Here we come!"),
                            new(
                            [
                                new(Riggs, "neutral", "Let's do this, Scarlet!")
                            ]
                            )
                    ]
                }
            },
            {
                "Wizard_Scarlet_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_wizard" ],
                    once = true,
                    priority = true,
                    bg = "BGWizard",
                    requiredScenes = [ "Wizard_1" ],
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                        new(Wizbo, "Shazam!", true),
                        new(Scarlet, "squint", "A wizard?"),
                        new(Wizbo, "A-ha! a fellow rat!", true),
                        new(Scarlet, "tired", "Sigh. False sorcery, eh?"),
                        new(Wizbo, "And you must be a pirate!", true),
                        new(Scarlet, "angry", "I am not a pirate!")
                    ]
                }
            },
            {
                "Wizard_Scarlet_1",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_wizard" ],
                    once = true,
                    priority = true,
                    bg = "BGWizard",
                    requiredScenes = [ $"Wizard_{Scarlet}_0" ],
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                    ]
                }
            },
            {
                "Wizard_Scarlet_2",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_wizard" ],
                    once = true,
                    priority = true,
                    bg = "BGWizard",
                    requiredScenes = [ $"Wizard_{Scarlet}_1" ],
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                    ]
                }
            },
            {
                "Knight_Scarlet_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_knight" ],
                    once = true,
                    priority = true,
                    bg = "BGCastle",
                    requiredScenes = [ "Knight_1" ],
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                        new(Ratzo, "A fellow rat!", true),
                        new(Scarlet, "squint", "Hey, careful with the hard \"R\" man..."),
                        new(Ratzo, "Thy dignity shant worry not. For Sir Ratzo too is a proud rat!", true),
                        new(Scarlet, "tired", "Sorry, but I'm more of a mouse."),
                        new(Ratzo, "Thy stature fails to speak for thyself!", true),
                        new(Scarlet, "neutral", "I guess I am pretty tall.")
                    ]
                }
            },
            {
                "Knight_Scarlet_1",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_knight" ],
                    once = true,
                    priority = true,
                    bg = "BGCastle",
                    requiredScenes = [ "Knight_Scarlet_0" ],
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                        new(Ratzo, "Young mouse, thy skill with daggers are remarkable!", true),
                        new(Scarlet, "dagger", "And thy blade too."),
                        new(Ratzo, "I seek a squire befitting my knighthood, doth thee accept this proposal?", true),
                        new(Scarlet, "dagger", "I deny."),
                        new(Scarlet, "daggertaunt", "Draweth thy blade so thee and me settle this debacle with honour."),
                        new(Ratzo, "Let it be so.", true)
                    ]
                }
            },
            {
                "Knight_Scarlet_2",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_knight" ],
                    once = true,
                    priority = true,
                    bg = "BGCastle",
                    requiredScenes = [ "Knight_Scarlet_1" ],
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                        new(Ratzo, "Young knave.", true),
                        new(Scarlet, "dagger", "Sir knight."),
                        new(Ratzo, "Where doth thee learneth sleight of hand with thy daggers?", true),
                        new(Scarlet, "My aunt."),
                        new(Ratzo, "Your aunt?!", true),
                        new(Scarlet, "daggertaunt", "Yes. She who taught me knife work, to drive daggers deep and tosseth."),
                        new(Ratzo, "She is quite the maiden.", true),
                        new(Scarlet, "daggertaunt", "I agree."),
                        new(Scarlet, "daggerangry", "Now die.")
                    ]
                }
            }
        }
        );
    }
}