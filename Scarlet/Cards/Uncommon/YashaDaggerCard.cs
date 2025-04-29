using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using Vionheart.Midrow;

namespace Vionheart.Cards;

public class YashaDaggerCard : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickDagger.png")); //Art used.
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "YashaDaggerCard", "name"]).Localize,
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
                cost = 2,
                recycle = true
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
                    thing = new YashaDagger_Missile
                    {
                    }
                },
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.A =>
            [
                new ASpawn
                {
                    thing = new YashaDagger_Missile
                    {
                    }
                },
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.B =>
            [
                new ASpawn
                {
                    thing = new YashaDagger_Missile
                    {
                    }
                },
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}