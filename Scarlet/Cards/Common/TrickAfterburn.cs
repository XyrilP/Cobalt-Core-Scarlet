using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Cards;

public class TrickAfterburn : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/TrickAfterburn.png")); //Art used.
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.common,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "TrickAfterburn", "name"]).Localize,
            Art = null
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
                retain = true,
                temporary = true,
                singleUse = true
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
                    status = Status.hermes,
                    targetPlayer = true,
                    statusAmount = 1
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
                    status = Status.hermes,
                    targetPlayer = true,
                    statusAmount = 1
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
                    status = Status.hermes,
                    targetPlayer = true,
                    statusAmount = 2
                },
                new AInstantTrick
                {
                    amount = 1
                }
            ],
            _ => []
        };
    }
}