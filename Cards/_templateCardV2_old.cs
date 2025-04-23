using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class _templateCardV2_old : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck, //Which deck should this card go to?
                rarity = Rarity.common, //What rarity should this card be?
                dontOffer = false, //Should this card be offered to the player?
                upgradesTo = [Upgrade.A, Upgrade.B] //Does this card upgrade? and if it has an A or B upgrade.
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Template", "name"]).Localize
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
                    break;
                case Upgrade.A:
                    break;
                case Upgrade.B:
                    break;
            }
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
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                };
                break;
        }
        return actions;
    }
}