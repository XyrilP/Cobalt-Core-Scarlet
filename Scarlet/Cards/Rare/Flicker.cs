using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Cards;

public class Flicker : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/Flicker.png")); //Art used.
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.rare,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Flicker", "name"]).Localize,
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
                cost = 0,
                temporary = true,
                singleUse = true
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                temporary = true,
                singleUse = true,
                retain = true
            },
            Upgrade.B => new CardData
            {
                cost = 0,
                temporary = true,
                singleUse = true
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
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = 2,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new AInstantTrick
                {
                    amount = 1
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = 2,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new AInstantTrick
                {
                    amount = 1
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = 2,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true,
                    isRandom = true
                },
                new AMove
                {
                    dir = 3,
                    targetPlayer = true,
                    isRandom = true
                },
                new AInstantTrick
                {
                    amount = 2
                }
            ],
            _ => []
        };
    }
}