using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class Veer : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "Veer", "name"]).Localize,
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
                    break;
                case Upgrade.A:
                    data.cost = 0;
                    break;
                case Upgrade.B:
                    data.cost = 1;
                    break;
            }
            data.flippable = true;
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
                    new AMove()
                    {
                        dir = 2,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    { 
                        dir = 2,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 4,
                        targetPlayer = true
                    },
                    /* v0.2.1
                    new AStatus()
                    {
                        status = veerAutoDodge,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                    */
                };
                break;
        }
        return actions;
    }
}