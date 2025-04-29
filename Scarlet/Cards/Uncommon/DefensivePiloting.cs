using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class DefensivePiloting : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = null; //Art used.
        FlippedArt1 = null; //Art used when card is flipped or flopped.
        FlippedArt2 = null;
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck, //Which deck should this card go to?
                rarity = Rarity.uncommon, //What rarity should this card be?
                dontOffer = false, //Should this card be offered to the player?
                upgradesTo = [Upgrade.A, Upgrade.B] //Does this card upgrade? and if it has an A or B upgrade.
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "DefensivePiloting", "name"]).Localize, //Card's name, localized.
            Art = BaseArt?.Sprite //Card art
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
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.shield,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.shield,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.shield,
                    statusAmount = 3,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}