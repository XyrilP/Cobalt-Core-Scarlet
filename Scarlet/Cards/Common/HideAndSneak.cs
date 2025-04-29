using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Vionheart.Cards;

public class HideAndSneak : Card, IRegisterable
{
    private static ISpriteEntry? BaseArt { get; set; }
    private static ISpriteEntry? FlippedArt1 { get; set; }
    private static ISpriteEntry? FlippedArt2 { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        BaseArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/HideAndSneak.png")); //Art used.
        FlippedArt1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/HideAndSneak_Right.png")); //Art used when card is flipped or flopped.
        FlippedArt2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/HideAndSneak_Left.png"));
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Vionheart.Instance.Scarlet_Deck.Deck, //Which deck should this card go to?
                rarity = Rarity.common, //What rarity should this card be?
                dontOffer = false, //Should this card be offered to the player?
                upgradesTo = [Upgrade.A, Upgrade.B] //Does this card upgrade? and if it has an A or B upgrade.
            },
            Name = Vionheart.Instance.AnyLocalizations.Bind(["card", "HideAndSneak", "name"]).Localize,
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
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 1,
                flippable = true
            },
            Upgrade.A => new CardData
            {
                art = !flipped ? FlippedArt1?.Sprite : FlippedArt2?.Sprite,
                cost = 0,
                flippable = true
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
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Vionheart.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletHideAndSneak"
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Vionheart.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletHideAndSneak"
                    },
                    new AMove()
                    {
                        dir = 1,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Vionheart.Instance.Fade.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        dialogueSelector = ".scarletHideAndSneak"
                    },
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true,
                    }
                };
                break;
        }
        return actions;
    }
}