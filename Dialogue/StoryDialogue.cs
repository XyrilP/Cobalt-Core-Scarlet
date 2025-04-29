using System.Collections.Generic;
using System.Security.Cryptography;
using VionheartScarlet;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal static class StoryDialogue
{
    internal static void Inject()
    {
        DB.story.all["Scarlet_Riggs_Intro_0"] = new()
		{
			type = NodeType.@event,
			lookup = new() {"zone_first"},
			once = true,
			allPresent = [ Scarlet, Riggs ],
			bg = "BGRunStart",
			lines = new()
            {
				new CustomSay()
				{
					who = Cat,
					Text = "BEEP BOOP, WAKE UP EVERYONE.",
                    loopTag = "transition"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Ughh.. my head.",
					loopTag = "squint"
				},
                new CustomSay()
				{
					who = Cat,
					Text = "Huh? who are you? I don't remember you in the manifest.",
                    loopTag = "squint"
				},
                new CustomSay()
				{
					who = Cat,
					Text = "...",
					loopTag = "worried"
				},
                new CustomSay()
				{
					who = Cat,
					Text = "Huh. Nevermind, I guess I was wrong.",
					loopTag = "squint"
				},
                new CustomSay()
				{
					who = Riggs,
					Text = "Uhhh, what's going on..?",
					loopTag = "nervous"
				},
                new CustomSay()
				{
					who = Scarlet,
					Text = "Not in the manifest..?",
					loopTag = "neutral"
				},
                new CustomSay()
				{
					who = Scarlet,
					Text = "I am a pilot.. am I not?",
					loopTag = "squint"
				},
                new CustomSay()
				{
					who = Cat,
					Text = "There's conveniently a new file here for a completely new manifest!",
					loopTag = "grumpy"
				},
                new CustomSay()
				{
					who = Cat,
					Text = "...",
					loopTag = "squint"
				},
                new CustomSay()
				{
					who = Cat,
					Text = "Welcome aboard, I guess!",
					loopTag = "smug"
				},
                new CustomSay()
				{
					who = Riggs,
					Text = "Hey! grab a seat!",
					loopTag = "neutral"
				}
            }
		};

		DB.story.all["Scarlet_PirateBoss_0"] = new()
		{
			type = NodeType.@event,
			lookup = new() {"before_pirateBoss"},
			once = true,
			allPresent = [ Scarlet ],
			bg = "BGRedGiant",
			lines = new()
            {
				new CustomSay()
				{
					who = Scarlet,
					Text = "...",
					loopTag = "squint"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Hey, ain't that the..?",
					loopTag = "squint"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "Scarlet.",
					loopTag = "embarrassed"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Riggs..?!",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "Hey..",
					loopTag = "embarrassed"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "What's going on here?!",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "You already know.",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "So put 'em up, pilot..!",
					loopTag = "serious"
				}
            }
		};

		DB.story.all["Scarlet_PirateBoss_1"] = new()
		{
			type = NodeType.@event,
			lookup = new() {"after_pirateBoss"},
			once = true,
			allPresent = [ Scarlet ],
			lines = new()
            {
				new CustomSay()
				{
					who = Scarlet,
					Text = "...",
					loopTag = "squint"
				},
				new CustomSay()
				{
					who = Cat,
					Text = "Are you okay?",
					loopTag = "worried"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "No.",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Cat,
					Text = "Do you need a break?",
					loopTag = "worried"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Yeah.",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Just for a lil' though. It's just...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "She really WAS her...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Cat,
					Text = "...",
					loopTag = "worried"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "The black jacket.",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "The purple eyeliner...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Cat,
					Text = "Sorry, we should go now.",
					loopTag = "worried"
				},
				new CustomSay()
				{
					who = Cat,
					Text = "We're on a tight schedule.",
					loopTag = "worried"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Yeah.",
					loopTag = "sad"
				}
            }
		};

		DB.story.all["Scarlet_Riggs_PirateBoss_0"] = new()
		{
			type = NodeType.@event,
			lookup = new() {"before_pirateBoss"},
			once = true,
			allPresent = [ Scarlet, Riggs ],
			bg = "BGRedGiant",
			lines = new()
            {
				new CustomSay()
				{
					who = Riggs,
					Text = "Mhm, mhm! Gosh, I've never been to the Rouall sector, maybe I'll take the Artemis out for a spin there!",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "...",
					loopTag = "squint",
					flipped = true
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Cool. They're pretty welcoming... You should try shawarma there.",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "...",
					loopTag = "squint",
					flipped = true
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "Ooh! I love shawarma!",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "...",
					loopTag = "squint",
					flipped = true
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "Are you two done?",
					loopTag = "serious",
					flipped = true
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "Gosh!",
					loopTag = "neutral"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "What? are you jealous?",
					loopTag = "nervous"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Riggs, hol' on now..",
					loopTag = "squint"
				},
				new CustomSay()
				{
					who = EvilRiggs,
					Text = "Oh that is it! He's mine, you fake time clone!",
					loopTag = "serious",
					flipped = true
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "Oh, she was actually jealous.",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Riggs! Other Riggs! Wait!",
					loopTag = "neutral" // Requires "distressed" emote
				}
            }
		};

		DB.story.all["Scarlet_Riggs_PirateBoss_1"] = new()
		{
			type = NodeType.@event,
			lookup = new() {"after_pirateBoss"},
			once = true,
			allPresent = [ Scarlet, Riggs ],
			lines = new()
            {
				new CustomSay()
				{
					who = Riggs,
					Text = "Hell yeah, baby!",
					loopTag = "gun"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "You okay?",
					loopTag = "nervous"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "You wanna talk about it?",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "She seems to like you?",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "...",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "I can put on purple makeup for you?",
					loopTag = "nervous"
				},
				new CustomSay()
				{
					who = Riggs,
					Text = "And a black leather jacket?",
					loopTag = "nervous"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Engines are primed.",
					loopTag = "sad"
				},
				new CustomSay()
				{
					who = Scarlet,
					Text = "Jumping to the next sector in 3... 2... 1...",
					loopTag = "sad"
				}
            }
		};
    }
}