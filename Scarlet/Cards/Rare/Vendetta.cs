using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Cards;

public class Vendetta : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/Vendetta.png"));
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/Vendetta_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/Vendetta_Left.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Vendetta", "name"]).Localize,
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
                cost = 2,
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 2,
                exhaust = true,
                flippable = true
            },
            Upgrade.B => new CardData
            {
                cost = 3,
                exhaust = true
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
                    isRandom = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                    weaken = true,
                },
                new ABackstab
                {
                    damage = GetDmg(s, 2)
                }
            ],
            Upgrade.A =>
            [
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                    weaken = true,
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
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true,
                },
                new AAttack
                {
                    damage = GetDmg(s, 1),
                    piercing = true,
                    brittle = true,
                },
                new ABackstab
                {
                    damage = GetDmg(s, 2)
                }
            ],
            _ => []
        };
    }
}