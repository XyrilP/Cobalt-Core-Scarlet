using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class FadeCard : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/FadeAway.png"));
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/FadeAway_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/FadeAway_Left.png"));
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "FadeCard", "name"]).Localize,
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
                cost = 3,
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                cost = 2,
                exhaust = true
            },
            Upgrade.B => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 3,
                exhaust = true,
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
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    dialogueSelector = ".scarletFadeAway"
                },
                new AMove
                {
                    dir = 6,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1
                },
                new AEndTurn
                {
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    dialogueSelector = ".scarletFadeAway"
                },
                new AMove
                {
                    dir = 6,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1
                },
                new AEndTurn
                {
                }
            ],
            Upgrade.B =>
            [
                new AStatus()
                {
                    status = VionheartScarlet.Instance.Fade.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    dialogueSelector = ".scarletFadeAway"
                },
                new AMove()
                {
                    dir = 2,
                    targetPlayer = true,
                },
                new AMove()
                {
                    dir = 4,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus()
                {
                    status = Status.lockdown,
                    statusAmount = 1
                },
                new AStatus()
                {
                    status = Status.evade,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}