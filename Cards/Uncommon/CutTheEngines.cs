using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace XyrilP.VionheartScarlet.Cards;

public class CutTheEngines : Card, IRegisterable
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
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["card", "CutTheEngines", "name"]).Localize,
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
            data.retain = true;
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
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.loseEvadeNextTurn,
                        targetPlayer = true,
                        statusAmount = 1
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.loseEvadeNextTurn,
                        targetPlayer = true,
                        statusAmount = 1
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;

        }
        return actions;


    }

}