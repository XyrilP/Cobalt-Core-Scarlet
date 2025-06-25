using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using HarmonyLib;

namespace VionheartScarlet.Artifacts;

public class VanguardBerthing : Artifact, IRegisterable
{
    public bool berthingInitialized { get; set; }
    public int berthingCardDraw { get; set; }
    public int berthingEnergyFragment { get; set; }
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact(new ArtifactConfiguration
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new ArtifactMeta
            {
                pools = [ArtifactPool.EventOnly],
                owner = Deck.colorless,
                unremovable = true
            },
            Name = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthing", "name"]).Localize,
            Description = VionheartScarlet.Instance.AnyLocalizations.Bind(["artifact", "VanguardBerthing", "description"]).Localize,
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/vanguard_berthing.png")).Sprite
        }
        );
    }
    public override void OnCombatStart(State state, Combat combat)
    {
        if (!berthingInitialized)
        {
            combat.QueueImmediate(
            [
                new AAddArtifact
                {
                    artifact = new VanguardBerthingInitial()
                }
            ]
            );
        }
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        var ship = state.ship;
        /* Draw additional cards */
        if (berthingCardDraw > 0)
        {
            combat.QueueImmediate(
            [
                new ADrawCard
                {
                    count = berthingCardDraw
                }
            ]
            );
        }
        /* Draw additional cards */
        /* Add Energy Fragment */
        if (berthingEnergyFragment > 0)
        {
            ship.Add(Status.energyFragment, berthingEnergyFragment);
            while (true)
            {
                int energyFragmentValue = ship.Get(Status.energyFragment);
                if (energyFragmentValue >= 3)
                {
                    combat.QueueImmediate(
                    [
                        new AEnergy
                        {
                            changeAmount = 1
                        }
                    ]
                    );
                    ship.Add(Status.energyFragment, -3);
                }
                else
                {
                    break;
                }
            }
        }
        /* Add Energy Fragment */
    }
    public override int? GetDisplayNumber(State s)
    {
        if (berthingCardDraw > 0)
        {
            return berthingCardDraw;
        }
        else
        {
            return null;
        }
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        if (berthingCardDraw != 0)
        {
            List<Tooltip> list = new List<Tooltip>();
            list.Add(new TTText(VionheartScarlet.Instance.Localizations.Localize(["artifact", "VanguardBerthing", "descExtra1"], new { TTberthingCardDraw = berthingCardDraw })));
            if (berthingEnergyFragment > 0)
            {
                list.Add(new TTText(VionheartScarlet.Instance.Localizations.Localize(["artifact", "VanguardBerthing", "descExtra2"], new { TTberthingEnergyFragment = berthingEnergyFragment })));
            }
            return list;
        }
        else
        {
            return null;
        }
    }
}