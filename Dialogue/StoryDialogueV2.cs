using System.Collections.Generic;
using System.Security.Cryptography;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal class StoryDialogueV2 : IRegisterable
{
	public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
	{
		LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {
                "Scarlet_DialogueMachineTest_1",
                new()
                {
                    type = NodeType.@event,
                    bg = "BGCafe",
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Apple bottom jeans."),
                        new(Riggs, "neutral", "Boots with the fur!", true),
                        new(Scarlet, "smug", "And she got me lookin' at her."),
                        new(Riggs, "bobaSlurp", "", true),
                        new(Brimford, "neutral", "We offer discounts to couples.", true),
                        new(Riggs, "neutral", "Awesome!"),
                        new(Brimford, "neutral", "And pirates too.", true),
                        new(Scarlet, "squint", "Sigh."),
                        new(Riggs, "gun", "")
                    ]
                }
            },
            {
                "Scarlet_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    allPresent = [ Scarlet ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Cat, "transition", "BEEP BOOP, WAKE UP EVERYONE.", true),
                        new(Scarlet, "tired", "Ughh.. my head."),
                        new(Cat, "squint", "Huh? who are you? I don't remember you in the manifest.", true),
                        new(Cat, "transition", "...", true),
                        new(Cat, "squint", "Huh. Nevermind, I guess I was wrong.", true),
                            new(
                            [
                                new(Dizzy, "shrug", "We make mistakes sometimes.", true),
                                new(Riggs, "nervous", "Uhhh, what's going on...?", true),
                                new(Hyperia, "squint", "Hmm.", true)
                            ]
                            ),
                        new(Scarlet, "neutral", "Not in the manifest...?\nI am a pilot, am I not?"),
                        new(Cat, "grumpy", "There's conveniently a new file here for a completely new manifest!", true),
                        new(Cat, "squint", "...", true),
                        new(Cat, "smug", "Welcome aboard, I guess!", true),
                            new(
                            [
                                new(Dizzy, "explains", "Feel free to pilot the ship, just don't blow it up.", true),
                                new(Riggs, "neutral", "Hey, pilot! Grab a seat right here in the cockpit!", true),
                                new(Hyperia, "squint", "I'll keep an eye on you.", true)
                            ]
                            ),
                        new(Scarlet, "tired", "Sigh.")
                    ]
                }
            },
            {
                "Scarlet_Dizzy_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Dizzy ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Dizzy, "neutral", "Red mouse guy!", true),
                        new(Scarlet, "tired", "Scarlet."),
                        new(Dizzy, "shrug", "Close enough!", true),
                        new(Scarlet, "tired", "..."),
                        new(Scarlet, "neutral", "How can I be of assistance?"),
                        new(Dizzy, "serious", "You're an effective pilot! how could you dodge with the ship like that?", true),
                        new(Scarlet, "neutral", "I can make the ship intangible."),
                        new(Dizzy, "squint", "...", true),
                        new(Scarlet, "neutral", "..."),
                        new(Dizzy, "squint", "What?", true),
                        new(Dizzy, "neutral", "Does it work with missiles?", true),
                        new(Scarlet, "tired", "..."),
                        new(Scarlet, "tired", "Yes.")
                    ]
                }
            },
            {
                "Scarlet_Riggs_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Riggs ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Riggs, "neutral", "Hey Scarlet!", true),
                        new(Scarlet, "happy", "Riggs."),
                        new(Riggs, "neutral", "I was wondering if you had any hobbies!", true),
                        new(Scarlet, "happy", "Hobbies! uhh..."),
                        new(Scarlet, "neutral", "I like driving cars and piloting ships."),
                        new(Scarlet, "squint", "I also juggle daggers."),
                        new(Riggs, "nervous", "Juggling daggers?", true),
                        new(Riggs, "nervous", "You don't accidentally cut yourself or anything like that, right?", true),
                        new(Scarlet, "blush", "What? No, of course not. I'm too good for that."),
                        new(Riggs, "neutral", "That's awesome!", true),
                        new(Scarlet, "flustered", "Thanks.")
                    ]
                }
            },
            {
                "Scarlet_Riggs_Intro_1",
                new()
                {
                    /*
                    Scarlet and Riggs are getting closer...
                    */
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Riggs_Intro_0" ],
                    allPresent = [ Scarlet, Riggs ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                    ]
                }
            },
            {
                "Scarlet_Riggs_Intro_2",
                new()
                {
                    /*
                    Scarlet and Riggs are really close!
                    */
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Riggs_Intro_1" ],
                    allPresent = [ Scarlet, Riggs ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                    ]
                }
            },
            {
                "Scarlet_Peri_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Hyperia ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Scarlet, "squint", "Hyperia."),
                        new(Hyperia, "mad", "Altear.", true),
                        new(Scarlet, "squint", "You know me?"),
                        new(Hyperia, "squint", "Enough from your files, Altear.", true),
                        new(Hyperia, "squint", "You work dangerously.", true),
                        new(Hyperia, "mad", "I can reason with pirates but-", true),
                        new(Scarlet, "angry", "I am not a pirate, Hyperia."),
                        new(Hyperia, "mad", "Maybe you're not, perhaps you're much worse.", true),
                        new(Scarlet, "tired", "Sigh."),
                        new(Scarlet, "tired", "Perhaps we got off the wrong foot, miss Hyperia."),
                        new(Hyperia, "squint", "Just Peri is fine.", true),
                        new(Scarlet, "tired", "Right..")
                    ]
                }
            },
            {
                "Scarlet_Isaac_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Isaac ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Hmm..."),
                        new(Scarlet, "neutral", "Oh? Isaac!"),
                        new(Isaac, "panic", "AH!", true),
                        new(Isaac, "squint", "Oh, Scarlet! I didn't notice you...", true),
                        new(Isaac, "squint", "You're so quiet and sneaky.", true),
                        new(Scarlet, "tired", "Sorry."),
                        new(Isaac, "shy", "So.. what is it?", true),
                        new(Scarlet, "neutral", "Nothing!"),
                        new(Isaac, "squint", "...", true),
                        new(Scarlet, "neutral", "Actually... I was curious about what you're doing with your drones?"),
                        new(Isaac, "explains", "Oh! I was just tinkering with them.", true),
                        new(Isaac, "shy", "And thinking of names for them...", true),
                        new(Scarlet, "squint", "You name your drones?"),
                        new(Isaac, "shy", "...", true),
                        new(Scarlet, "neutral", "Can I name one?"),
                        new(Isaac, "writing", "Ohoh? go ahead?", true),
                        new(Scarlet, "squint", "Okay! hmmmm..."),
                        new(Scarlet, "happy", "How about we name this one \"Nick\"?"),
                        new(Isaac, "writing", "Ooh, good one. Hello Nick!", true)
                    ]
                }
            },
            {
                "Scarlet_Eunice_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Eunice ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Eunice, "sly", "Hey pirate!", true),
                            new(
                            [
                                new(Hyperia, "mad", "I knew it!", true)
                            ]
                            ),
                        new(Scarlet, "tired", "Sigh."),
                        new(Scarlet, "tired", "Eunice."),
                        new(Scarlet, "squint", "I am not a pirate."),
                        new(Eunice, "neutral", "Oh... right!", true),
                        new(Eunice, "sly", "We are not pirates.", true),
                        new(Eunice, "sadEyesClosed", "Wink. Wink.", true),
                        new(Scarlet, "tired", "Ugh."),
                        new(Eunice, "sly", "Nudge. Nudge.", true),
                        new(Scarlet, "angry", "Eunice, knock it off!"),
                        new(Eunice, "slyBlush", "Okay.", true)
                    ]
                }
            },
            {
                "Scarlet_Max_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Max ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Scarlet, "neutral", "Hey."),
                        new(Max, "intense", "GUH?!", true),
                        new(Max, "mad", "Hey, don't sneak up on me like that!", true),
                        new(Scarlet, "nervous", "Uh- I wasn't...?"),
                        new(Max, "squint", "Nevermind...", true),
                        new(Max, "neutral", "What's up, man?", true),
                        new(Scarlet, "neutral", "You're the technomancer, right?"),
                        new(Max, "smile", "Yeah, sure.", true),
                        new(Max, "gloves", "I'm something of a technomancer myself.", true),
                        new(Scarlet, "happy", "Nice."),
                        new(Scarlet, "neutral", "I'm a pilot, so I'll make sure we don't get hit and the cannons are aligned, hacker-man."),
                        new(Max, "smile", "Niiiiice.", true)
                    ]
                }
            },
            {
                "Scarlet_Books_Intro_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    requiredScenes = [ "Scarlet_Intro_0" ],
                    allPresent = [ Scarlet, Books ],
                    once = true,
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(Books, "neutral", "Mister Altear! Mister Altear!", true),
                        new(Scarlet, "squint", "Huh? Who...?"),
                        new(Books, "paws", "It's me, Books! remember?", true),
                        new(Scarlet, "neutral", "Oh hey, Books...?"),
                        new(Books, "paws", "We went on adventures together!", true),
                        new(Books, "blush", "Like when Triune told me to search around in the Drift Plane and found your crew and your starship sliced in halves fighting against Miss Drake?", true),
                        new(Books, "blush", "Or when I met Mister Solivane for the first time when I was playing VURS with my dream-sharing friends in Little Akiton, at Absalom Station?", true),
                        new(Books, "books", "Maybe when I was with your sister halfway across a planet trying to buy parmesan cheese?", true),
                        new(Books, "books", "Oh wait, you were in jail at the time...", true),
                        new(Scarlet, "nervous", "Uhm."),
                        new(Books, "squint", "Oh I see how it is.", true),
                        new(Books, "plan", "Don't worry Mister Altear, I have just the thing!", true),
                        new(Books, "crystal", "", true),
                        new(Scarlet, "scream", "ISN'T THAT EXTREMELY RADIOACTIVE?!"),
                        new(Books, "crystal", "Don't be so worried Mister Altear, it is radioactive!", true),
                        new(Scarlet, "angry", "Hey!! Get that thing away from me!!!"),
                        new(Books, "stoked", "It's for your own good, Mister Altear!!", true)
                    ]
                }
            },
            {
                "Scarlet_Dizzy_AfterCrystal_0",
                new()
                {
                    type = NodeType.@event,
                    requiredScenes = [ "PostCrystal_1" ],
                    lookup = [ "after_crystal" ],
                    allPresent = [ Scarlet, Dizzy ],
                    once = true,
                    bg = "BGCrystalNebula",
                    dialogue =
                    [
                        new(Dizzy, "neutral", "Hey Scarlet!"),
                        new(Scarlet, "neutral", "Dizzy...?"),
                        new(Scarlet, "neutral", "What's up?"),
                        new(Dizzy, "geiger", "You're emitting a lot of radiation!", true),
                        new(Scarlet, "tired", "Ehm... I think everyone is emitting a lot of radiation recently."),
                        new(Dizzy, "neutral", "That is true!", true),
                        new(Dizzy, "explains", "The crystals themselves emit a lot of radiation, and we are constantly exposed to such things.", true),
                        new(Scarlet, "neutral", "Is it the bad kind of radiation or the good kind?"),
                        new(Dizzy, "crystal", "", true),
                        new(Scarlet, "angry", "DIZZY!")
                    ]
                }
            },
            {
                "Scarlet_PirateBoss_0",
                new()
                {
                    type = NodeType.@event,
                    lookup = [ "before_pirateBoss" ],
                    allPresent = [ Scarlet ],
                    once = true,
                    bg = "BGRedGiant",
                    dialogue =
                    [
                        new(Scarlet, "squint", "..."),
                        new(Scarlet, "squint", "Hey, ain't that the...?"),
                        new(EvilRiggs, "embarrassed", "Scarlet.", true),
                        new(Scarlet, "neutral", "Riggs...?"),
                        new(EvilRiggs, "embarrassed", "Hey...", true),
                        new(Scarlet, "neutral", "What's goin' on here?"),
                        new(EvilRiggs, "neutral", "You already know.", true),
                        new(EvilRiggs, "serious", "So put 'em up, pilot...!", true),
                        new(Scarlet, "sad", "Hang on! Wait!")
                    ]
                }
            },
            {
                "Scarlet_PirateBoss_1",
                new()
                {
                    type = NodeType.@event,
                    requiredScenes = [ "Scarlet_PirateBoss_0" ],
                    lookup = [ "after_pirateBoss" ],
                    once = true,
                    allPresent = [ Scarlet ],
                    dialogue =
                    [
                        new(Scarlet, "sad", "..."),
                        new(Cat, "worried", "Are you okay?", true),
                        new(Scarlet, "sad", "No."),
                        new(Cat, "worried", "Do you need a break?", true),
                        new(Scarlet, "sad", "Yeah."),
                        new(Scarlet, "focus", "..."),
                        new(Scarlet, "focus", "Just for a lil' though. It's just..."),
                        new(Scarlet, "focus", "She really WAS her..."),
                        new(Cat, "worried", "...", true),
                        new(Scarlet, "focus", "The black jacket."),
                        new(Scarlet, "focus", "The purple eyeliner."),
                        new(Cat, "worried", "I'm sorry but we should go now.", true),
                        new(Cat, "worried", "And we're on a tight schedule.", true),
                        new(Scarlet, "tired", "Yeah. Let's get going.")
                    ]
                }
            },
            {
                "Scarlet_Riggs_PirateBoss_0",
                new()
                {
                    type = NodeType.@event,
                    requiredScenes = [ "Scarlet_PirateBoss_0", "Scarlet_Riggs_Intro_2", "PirateBoss_1" ],
                    lookup = [ "before_pirateBoss" ],
                    once = true,
                    allPresent = [ Scarlet, Riggs ],
                    bg = "BGRedGiant",
                    dialogue =
                    [
                        new(Scarlet, "happy", "... So that's the Rouall sector, basically."),
                        new(Riggs, "neutral", "Mhm, mhm! Gosh, I've never been to that sector before.", true),
                        new(Riggs, "neutral", "Maybe I'll take the Artemis out for a spin there!", true),
                        new(EvilRiggs, "squint", "...", true),
                        new(Scarlet, "happy", "Cool. It's a welcome sight, and you should try shawarma there."),
                        new(EvilRiggs, "squint", "...", true),
                        new(Riggs, "neutral", "Ooh! I love shawarma!", true),
                        new(EvilRiggs, "squint", "...", true),
                        new(EvilRiggs, "serious", "Are you two done?", true),
                        new(Riggs, "neutral", "Gosh!", true),
                        new(Riggs, "nervous", "What? Are you jealous?"),
                        new(Scarlet, "nervous", "Riggs, hol' on now..."),
                        new(EvilRiggs, "serious", "Oh that is it! He's mine, you fake time clone!", true),
                        new(Riggs, "sad", "Oh, she was actually jealous."),
                        new(Scarlet, "scream", "Riggs! Other Riggs! Wait!"),
                        new(EvilRiggs, "serious", "I'LL BURY THE TWO OF YOU IN SCRAP!", true)
                    ]
                }
            },
            {
                "Scarlet_Riggs_PirateBoss_1",
                new()
                {
                    type = NodeType.@event,
                    requiredScenes = [ "Scarlet_Riggs_PirateBoss_0" ],
                    lookup = [ "after_pirateBoss" ],
                    once = true,
                    allPresent = [ Scarlet, Riggs ],
                    dialogue =
                    [
                        new(Riggs, "gun", "Hell yeah, baby!", true),
                        new(Scarlet, "sad", "..."),
                        new(Riggs, "nervous", "You okay?", true),
                        new(Riggs, "sad", "You wanna talk about it?", true),
                        new(Scarlet, "sad", "..."),
                        new(Riggs, "sad", "She seemed to like you, huh?", true),
                        new(Scarlet, "focus", "..."),
                        new(Riggs, "sad", "...", true),
                        new(Riggs, "nervous", "I... I can put on purple makeup for you?", true),
                        new(Riggs, "nervous", "And a black leather jacket?", true),
                        new(Scarlet, "tired", "Engines are primed."),
                        new(Riggs, "sad", "...", true),
                        new(Scarlet, "tired", "Jumping into the next sector in 3... 2... 1...")
                    ]
                }
            }
        }
		);
    }
}