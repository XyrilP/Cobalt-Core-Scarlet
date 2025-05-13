using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using VionheartScarlet.Midrow;

namespace VionheartScarlet.Cards;

public class RunAndGun : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/RunAndGun.png")); //Art used.
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "RunAndGun", "name"]).Localize,
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
                cost = 0
            },
            Upgrade.B => new CardData
            {
                cost = 1
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
                    status = VionheartScarlet.Instance.scarletBarrage.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.scarletBarrage.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.scarletBarrage.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AMove
                {
                    dir = -1,
                    targetPlayer = true
                },
                new AMove
                {
                    dir = 2,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}