using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VionheartScarlet.Cards;

public class ScarletEXE : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                rarity = Rarity.common,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "ScarletEXE", "name"]).Localize
        }
        );
    }
    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.None => new CardData
            {
                cost = 1,
                exhaust = true,
                description = VionheartScarlet.Instance.Localizations.Localize(["card", "ScarletEXE", "description"]),
                artTint = "BC2C3D"
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                exhaust = true,
                description = VionheartScarlet.Instance.Localizations.Localize(["card", "ScarletEXE", "descA"]),
                artTint = "BC2C3D"
            },
            Upgrade.B => new CardData
            {
                cost = 1,
                exhaust = true,
                description = VionheartScarlet.Instance.Localizations.Localize(["card", "ScarletEXE", "descB"]),
                artTint = "BC2C3D"
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
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    dialogueSelector = ".summonScarlet"
                }
            ],
            Upgrade.A =>
            [
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    dialogueSelector = ".summonScarlet"
                }
            ],
            Upgrade.B =>
            [
                new ACardOffering
                {
                    amount = 3,
                    limitDeck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    dialogueSelector = ".summonScarlet"
                }
            ],
            _ => []
        };
    }
}