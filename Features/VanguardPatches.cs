using System;
using System.Linq;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;

namespace VionheartScarlet.Features;

public class VanguardPatches
{
    public VanguardPatches(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAddCharacter), nameof(AAddCharacter.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AllowMoreThanThreeCrew))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Character), nameof(Character.RenderCharacters)),
            prefix: new HarmonyMethod(GetType(), nameof(RearrageCrewUI))
        );
        VionheartScarlet.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Character), nameof(Character.Render)),
            postfix: new HarmonyMethod(GetType(), nameof(RenderMissingStatusOnMinis))
        );
        // VionheartScarlet.Instance.Harmony.Patch(
        //     original: AccessTools.DeclaredMethod(typeof(Character), nameof(Character.RenderCharacters)),
        //     prefix: new HarmonyMethod(GetType(), nameof(MakePortraitsMini))
        // );
    }
    public static void AllowMoreThanThreeCrew(AAddCharacter __instance, G g, State s, Combat c)
    {
        var aaddcharacter = __instance;
        var vanguardShip = VionheartScarlet.Instance.Vanguard_Ship.UniqueName;
        if (s.ship.key == vanguardShip)
        {
            aaddcharacter.canGoPastTheCharacterLimit = true;
        }
    }
    public static bool RearrageCrewUI(Character __instance, G g, Vec offset, bool mini, double introTimer, bool showDialogue, ref bool finaleMode)
    {
        var state = g.state;
        var vanguardShip = VionheartScarlet.Instance.Vanguard_Ship.UniqueName;
        if (state.ship.key != vanguardShip)
        {
            return true;
        }
        if (finaleMode || state.characters.Count > 3)
        {
            mini = true;
        }
        Rect? rect = default(Rect) + offset;
        g.Push(null, rect, null, autoFocus: false, noHoverSound: false, gamepadUntargetable: false, ReticleMode.Quad, null, null, null, null, 0, null, null, null, null);
        int num_y = 0;
        int num_x = 0;
        int characterCount = 0;
        int characterCountStack = 0;
        int characterCountStack2 = 0;
        bool characterCountLastStack = false;
        for (int i = 0; i < g.state.characters.Count; i++)
        {
            Character character = g.state.characters[i];
            int x = num_x + (int)Mutil.AnimHelper(introTimer, -90.0, 0.0, 360.0, 0.0 + (double)i * 0.1);
            int y = num_y;
            bool mini2 = mini;
            bool isExploding = g.state.ship.hull <= 0;
            bool isDoneExploding = g.state.ship.deathProgress >= 1.0;
            bool hasValue = g.state.ship.escapeTimer.HasValue;
            bool showDialogue2 = showDialogue;
#pragma warning disable DirectGameEnumUsage // Avoid direct game enum usage
            UIKey? upHint = ((i == 0) ? new UIKey?(UK.corner_mainmenu) : ((UIKey?)null));
#pragma warning restore DirectGameEnumUsage // Avoid direct game enum usage
            double blurbAlign = -1.0;
            character.Render(g, x, y, flipX: false, mini2, isExploding, isDoneExploding, hasValue, null, renderLocked: false, hideFace: false, showUnlockInstructions: false, showDialogue2, null, null, null, upHint, null, canFocus: true, null, autoFocus: false, showTooltips: true, i switch
            {
                0 => -10,
                1 => -20,
                2 => -30,
                3 => -10,
                4 => -20,
                5 => -30,
                _ => -10
            }, blurbAlign);
            if (finaleMode || g.state.characters.Count > 3)
            {
                // if (characterCount == 0 && characterInitial)
                // {
                //     num_y += 32;
                //     characterInitial = false;
                // }
                if (characterCount < 2)
                {
                    characterCount++;
                    num_y += 32;
                }
                else if (characterCountStack < 1)
                {
                    characterCountStack++;
                    characterCount = 0;
                    num_y -= 32 * 2;
                    num_x += 34;
                }
                else if (characterCountStack2 < 1)
                {
                    characterCountStack2++;
                    characterCountStack = 0;
                    characterCount = 0;
                    num_y += 32;
                    num_x = 0;
                }
                else if (!characterCountLastStack)
                {
                    characterCountLastStack = true;
                    // characterCount = 0;
                    num_y = -32;
                    num_x += 34;
                }
                if (characterCountLastStack)
                {
                    num_y += 32;
                }
                mini = true;
            }
            else
            {
                num_y += mini ? 32 : 60;
            }
        }
        g.Pop();
        return false;
    }
    public static void RenderMissingStatusOnMinis(Character __instance, G g, int x, int y, bool mini, bool flipX)
    {
        if (g.state.route is Combat && g.state.CharacterIsMissing(__instance.deckType) && mini)
        {
            int index = (int)Mutil.Mod(Math.Floor(g.state.time * 12.0 + (double)x + (double)y), (double)Character.noiseSprites.Count);
            Spr? id = Character.noiseSprites[index];
            Rect xy = g.Peek(null);
            double x2 = xy.x + (double)(flipX ? 1 : 4) + (double)(mini ? (-1) : 0);
            double y2 = xy.y + 1.0;
            Color color = Colors.textMain;
            Rect? pixelRect = (mini ? new Rect?(new Rect(0.0, 0.0, 29.0, 29.0)) : ((Rect?)null));
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Draw.Sprite(id, x2, y2, false, false, 0.0, (Vec?)null, (Vec?)null, (Vec?)null, pixelRect, (Color?)color, (BlendState)null, (SamplerState)null, (Effect)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }
    }
    // public static void MakePortraitsMini(G g, ref bool mini, bool finaleMode)
    // {
    //     if (!finaleMode && g.state.characters.Count() > 3)
    //     {
    //         mini = true;
    //     }
    // }
}
public static class GExt
{
	public static Rect Peek(this G g, Rect? rect = null)
	{
		Box? box = (g.uiStack.Count > 0) ? g.uiStack.Peek() : null;
		Rect valueOrDefault = rect.GetValueOrDefault();
		Rect? val = box?.rect;
		return (val.HasValue ? new Rect?(valueOrDefault + val.GetValueOrDefault()) : ((Rect?)null)).GetValueOrDefault();
	}
}