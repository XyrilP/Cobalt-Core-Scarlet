using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using VionheartScarlet.Midrow;

namespace VionheartScarlet.Cards;

public class SangeDaggerCard : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/SangeDagger.png")); //Art used.
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/SangeDagger_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/SangeDagger_Left.png"));
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "SangeDaggerCard", "name"]).Localize,
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
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new ASpawn
                {
                    thing = new ScarletDagger_Missile
                    {
                        missileType = MissileType.seeker
                    }
                }
            ],
            Upgrade.A =>
            [
                new ASpawn
                {
                    thing = new ScarletDagger_Missile
                    {
                        missileType = MissileType.seeker
                    }
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new ASpawn
                {
                    thing = new ScarletDagger_Missile
                    {
                        missileType = MissileType.seeker
                    }
                }
            ],
            Upgrade.B =>
            [
                new AMove
                {
                    dir = 3,
                    targetPlayer = true
                },
                new ASpawn
                {
                    thing = new ScarletDagger_Missile
                    {
                        missileType = MissileType.seeker
                    }
                }
            ],
            _ => []
        };
    }
}