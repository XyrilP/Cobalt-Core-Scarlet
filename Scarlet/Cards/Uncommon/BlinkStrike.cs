using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Cards;

public class BlinkStrike : Card, IRegisterable
{
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/BlinkStrike_Right.png"));
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/BlinkStrike_Left.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "BlinkStrike", "name"]).Localize,
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 1,
                flippable = true,
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                flippable = true,
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite
            },
            Upgrade.B => new CardData
            {
                cost = 1,
                flippable = true,
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite
            },
            _ => new CardData{}
        };
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None =>
            [
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    dialogueSelector = ".scarletBlinkStrike"
                },
                new ABackstab
                {
                    damage = GetDmg(s, 1)
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    dialogueSelector = ".scarletBlinkStrike"
                },
                new ABackstab
                {
                    damage = GetDmg(s, 2)
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = -2,
                    targetPlayer = true,
                    dialogueSelector = ".scarletBlinkStrike"
                },
                new ABackstab
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 4,
                    targetPlayer = true
                },
                new ABackstab
                {
                    damage = GetDmg(s, 1)
                }
            ],
            _ => []
        };
    }
}