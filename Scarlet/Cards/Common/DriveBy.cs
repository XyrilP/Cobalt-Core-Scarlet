using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class DriveBy : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/DriveBy.png")); //Art used.
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/DriveBy_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/DriveBy_Left.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "DriveBy", "name"]).Localize,
            Art = BaseArt?.Sprite
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 2
            },
            Upgrade.A => new CardData
            {
                cost = 1
            },
            Upgrade.B => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 2,
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
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                }
            ],
            Upgrade.A =>
            [
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 1)
                }
            ],
            Upgrade.B =>
            [
                new AAttack
                {
                    damage = GetDmg(s, 2)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true
                },
                new AAttack
                {
                    damage = GetDmg(s, 2)
                }
            ],
            _ => []
        };
    }
}