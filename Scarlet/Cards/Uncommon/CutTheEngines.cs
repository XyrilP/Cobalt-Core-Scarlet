using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class CutTheEngines : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/CutTheEngines.png"));
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
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "CutTheEngines", "name"]).Localize,
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
                cost = 1,
                retain = true
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
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = Status.evade
                },
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = s.ship.Get(Status.evade),
                    xHint = 1
                },
                new AStatus
                {
                    status = Status.evade,
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = Status.evade
                },
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = s.ship.Get(Status.evade),
                    xHint = 1
                },
                new AStatus
                {
                    status = Status.evade,
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set
                }
            ],
            Upgrade.B =>
            [
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = Status.evade
                },
                new AStatus
                {
                    status = Vionheart.Instance.Fade.Status,
                    targetPlayer = true,
                    statusAmount = s.ship.Get(Status.evade),
                    xHint = 1
                },
                new AStatus
                {
                    status = Status.evade,
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set
                }
            ],
            _ => []
        };
    }
}