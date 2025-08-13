using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class BulletHell : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/BulletHell.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck, //Which deck should this card go to?
                rarity = Rarity.rare, //What rarity should this card be?
                dontOffer = false, //Should this card be offered to the player?
                upgradesTo = [Upgrade.A, Upgrade.B] //Does this card upgrade? and if it has an A or B upgrade.
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "BulletHell", "name"]).Localize, //Card's name, localized.
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
                cost = 4,
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                cost = 3,
                exhaust = true
            },
            Upgrade.B => new CardData
            {
                cost = 4,
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
                    status = VionheartScarlet.Instance.SaturationBarrage.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AAttack
                {
                    damage = GetDmg(s,1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.SaturationBarrage.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AAttack
                {
                    damage = GetDmg(s,1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = VionheartScarlet.Instance.SaturationBarrage.Status,
                    statusAmount = 3,
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AAttack
                {
                    damage = GetDmg(s,1)
                },
                new AMove
                {
                    dir = 1,
                    targetPlayer = true,
                    isRandom = true
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            _ => []
        };
    }
}