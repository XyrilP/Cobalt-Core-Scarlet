using System.Collections.Generic;
using System.Security.Cryptography;
using Nanoray.PluginManager;
using Nickel;
using VionheartScarlet;
using static VionheartScarlet.Dialogue.CommonDefinitions;

namespace VionheartScarlet.Dialogue;

internal class MemoryDialogue : IRegisterable
{
	public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
	{
		LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {
                "RunWinWho_Scarlet_0",
                new()
                {
                    type = NodeType.@event,
                    introDelay = false,
                    allPresent = [ Scarlet ],
                    bg = "BGRunWin",
                    lookup = [ $"runWin_{Scarlet}" ],
                    dialogue =
                    [
                        new(new Wait{ secs = 3}),
                        new(Triune, "neutral", "It is not time yet.", true),
                        new(Scarlet, "tired", "Darn it...")
                    ]
                }
            },
            {
                "RunWinWho_Scarlet_1",
                new()
                {
                    type = NodeType.@event,
                    introDelay = false,
                    allPresent = [ Scarlet ],
                    bg = "BGRunWin",
                    lookup = [ $"runWin_{Scarlet}" ],
                    requiredScenes = [ "RunWinWho_Scarlet_0" ],
                    dialogue =
                    [
                        new(new Wait{ secs = 3}),
                        new(Triune, "neutral", "It is not time yet.", true),
                        new(Scarlet, "tired", "Darn it...")
                    ]
                }
            },
            {
                "RunWinWho_Scarlet_2",
                new()
                {
                    type = NodeType.@event,
                    introDelay = false,
                    allPresent = [ Scarlet ],
                    bg = "BGRunWin",
                    lookup = [ $"runWin_{Scarlet}" ],
                    requiredScenes = [ "RunWinWho_Scarlet_1" ],
                    dialogue =
                    [
                        new(new Wait{ secs = 3}),
                        new(Triune, "neutral", "It is not time yet.", true),
                        new(Scarlet, "tired", "Darn it...")
                    ]
                }
            },
            {
                "Scarlet_Memory_0",
                new()
                {
                    type = NodeType.@event,
                    introDelay = false,
                    allPresent = [ Scarlet ],
                    bg = null,
                    lookup = [ "vault", $"vault_{Scarlet}" ],
                    dialogue =
                    [
                        new("T-??? days"),
                        new(new Wait{ secs = 2}),
                        new(Triune, "neutral", "It is not time yet.", true),
                        new(Scarlet, "tired", "Darn it...")
                    ]
                }
            },
            {
                "Scarlet_Memory_1",
                new()
                {
                    type = NodeType.@event,
                    introDelay = false,
                    allPresent = [ Scarlet ],
                    bg = null,
                    lookup = [ "vault", $"vault_{Scarlet}" ],
                    requiredScenes = [ "Scarlet_Memory_0" ],
                    dialogue =
                    [
                        new("T-??? days"),
                        new(new Wait{ secs = 2}),
                        new(Triune, "neutral", "It is not time yet.", true),
                        new(Scarlet, "tired", "Darn it...")
                    ]
                }
            },
            {
                "Scarlet_Memory_2",
                new()
                {
                    type = NodeType.@event,
                    introDelay = false,
                    allPresent = [ Scarlet ],
                    bg = null,
                    lookup = [ "vault", $"vault_{Scarlet}" ],
                    requiredScenes = [ "Scarlet_Memory_1" ],
                    dialogue =
                    [
                        new("T-??? days"),
                        new(new Wait{ secs = 2}),
                        new(Triune, "neutral", "It is not time yet.", true),
                        new(Scarlet, "tired", "Darn it...")
                    ]
                }
            }
        }
		);
    }
}