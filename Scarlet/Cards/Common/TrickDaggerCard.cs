using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using VionheartScarlet.Midrow;

namespace VionheartScarlet.Cards;

public class TrickDaggerCard : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickDagger.png")); //Art used.
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickDagger_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickDagger_Left.png"));
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "TrickDaggerCard", "name"]).Localize,
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
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 1,
                flippable = true
            },
            Upgrade.A => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 1,
                flippable = true
            },
            Upgrade.B => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 1,
                flippable = true,
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
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                    },
                    offset = 1
                },
                new AStatus
                {
                    status = Status.droneShift,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.A =>
            [
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                    },
                    offset = 1
                },
                new AStatus
                {
                    status = Status.droneShift,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.B =>
            [
                new ASpawn
                {
                    thing = new TrickDagger_Missile
                    {
                    },
                    offset = 1
                },
                new AStatus
                {
                    status = Status.droneShift,
                    statusAmount = 3,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}