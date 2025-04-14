using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class UncannyDodge : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = VionheartScarlet.Instance.Scarlet_Deck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "UncannyDodge", "name"]).Localize,
        }
        );
        
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        {
            data.retain = true;
            data.cost = 0;
            switch (upgrade)
            {
                case Upgrade.None:
                    break;
                case Upgrade.A:
                    break;
                case Upgrade.B:
                    data.exhaust = true;
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
                    new AStatus()
                    {
                        status = Status.autododgeRight,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.autododgeRight,
                        statusAmount = 2,
                        targetPlayer = true,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.autododgeLeft,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                };
                break;
        }
        return actions;
    }

}