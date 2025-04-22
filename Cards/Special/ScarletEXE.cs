using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

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

        CardData data = new CardData();
        {
            switch (upgrade)
            {
                case Upgrade.None:
                    data.cost = 1;
                    data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "ScarletEXE", "description"]);
                    break;
                case Upgrade.A:
                    data.cost = 0;
                    data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "ScarletEXE", "descA"]);
                    break;
                case Upgrade.B:
                    data.cost = 1;
                    data.description = VionheartScarlet.Instance.Localizations.Localize(["card", "ScarletEXE", "descB"]);
                    break;
            }
            data.artTint = "BC2C3D";
            data.exhaust = true;
        }
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
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
                };
                break;
            case Upgrade.A:
                actions = new()
                {
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
                };
                break;
            case Upgrade.B:
                actions = new()
                {
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
                };
                break;
        }
        return actions;
    }
}