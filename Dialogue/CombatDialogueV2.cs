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
            }
        }
        );
    }
}