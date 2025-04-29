using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class SneakAttack : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/SneakAttack.png")); //Art used.
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/SneakAttack_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/SneakAttack_Left.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "SneakAttack", "name"]).Localize,
            Art = BaseArt.Sprite
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 1
            },
            Upgrade.A => new CardData
            {
                cost = 1
            },
            Upgrade.B => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 1,
                flippable = true
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
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    dialogueSelector = ".scarletSneakAttack"
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true,
                    dialogueSelector = ".scarletSneakAttack"
                },
                new AAttack
                {
                    damage = GetDmg(s, 2),
                    piercing = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    dialogueSelector = ".scarletSneakAttack"
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                },
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ],
            _ => []
        };
    }
}