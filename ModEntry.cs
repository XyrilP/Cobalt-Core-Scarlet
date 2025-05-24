using Nickel;
using Nickel.Common;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using XyrilP.ExternalAPI;
using VionheartScarlet.Features;
using VionheartScarlet.Cards;
using VionheartScarlet.Dialogue;
using VionheartScarlet.Artifacts;
using VionheartScarlet.Events;



namespace VionheartScarlet;
internal class VionheartScarlet : SimpleMod
{
    /* Declare stuff! */

    internal static VionheartScarlet Instance { get; private set; } = null!;
    internal Harmony Harmony;
    internal IKokoroApi.IV2 KokoroApi;
    internal IMoreDifficultiesApi? MoreDifficultiesApi { get; private set; } = null;
    internal IDuoArtifactsApi? DuoArtifactsApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
    public bool modDialogueInited;
    internal string UniqueName { get; private set; }
    public LocalDB localDB { get; set; } = null!;

    /* Vionheart Content */
    private static List<Type> Colorless_Common_Card_Types = [
        /* Common cards. */
    ];
    private static List<Type> Colorless_Uncommon_Card_Types = [
        /* Uncommon cards. */
    ];
    private static List<Type> Colorless_Rare_Card_Types = [
        /* Rare cards. */
    ];
    private static List<Type> Colorless_Special_Card_Types = [
        /* Special cards. */
    ];
    private static IEnumerable<Type> Colorless_All_Card_Types =
        Colorless_Common_Card_Types
            .Concat(Colorless_Uncommon_Card_Types)
            .Concat(Colorless_Rare_Card_Types)
            .Concat(Colorless_Special_Card_Types);
    private static List<Type> Colorless_Common_Artifact_Types = [
        /* Common artifacts. */
    ];
    private static List<Type> Colorless_Boss_Artifact_Types = [
        /* Boss artifacts. */
    ];
    private static List<Type> Colorless_Event_Artifact_Types = [
        /* Event artifacts. */
        typeof(TwoPilots)
    ];
    private static IEnumerable<Type> Colorless_All_Artifact_Types =
        Colorless_Common_Artifact_Types
            .Concat(Colorless_Boss_Artifact_Types)
            .Concat(Colorless_Event_Artifact_Types);
    private static List<Type> Ship_Artifact_Types = [
        /* Ship artifacts */
        typeof(VanguardBerthing),
        typeof(VanguardBerthingInitial),
        typeof(VanguardBerthingPlus)
    ];
    internal static IReadOnlyList<Type> Event_Types { get; } = [
        /* Events */
        typeof(VanguardBerthingEvent),
        typeof(Scarlet_Riggs_Date)
    ];
    internal static List<Type> Dialogue_Types = [
        typeof(StoryDialogueV2),
        typeof(CombatDialogueV2),
        typeof(EventDialogueV2)
    ];
    private static IEnumerable<Type> Vionheart_Content =
        Colorless_All_Card_Types
            .Concat(Colorless_All_Artifact_Types)
            .Concat(Ship_Artifact_Types)
            .Concat(Event_Types)
            .Concat(Dialogue_Types);
    /* Vionheart Content */
    /* Scarlet Content */
    internal IDeckEntry Scarlet_Deck; //Scarlet's Deck of Cards
    internal IStatusEntry Fade { get; }
    internal IStatusEntry scarletBarrage { get; }
    internal IStatusEntry Saturation { get; }

    internal ISpriteEntry TrickDagger { get; }
    internal ISpriteEntry TrickDagger_Icon { get; }
    internal ISpriteEntry TrickDagger_Seeker_Animated { get; }
    internal ISpriteEntry TrickDagger_Seeker { get; }
    internal ISpriteEntry TrickDagger_Seeker_Angled { get; }
    internal ISpriteEntry TrickDagger_Seeker_Icon { get; }
    internal ISpriteEntry TrickDagger_Heavy { get; }
    internal ISpriteEntry TrickDagger_Heavy_Icon { get; }
    private static List<Type> Scarlet_Common_Card_Types = [
        /* Scarlet's common cards. */
        typeof(Veer),
        typeof(DriveBy),
        typeof(AdjustThrottle),
        typeof(SneakAttack),
        typeof(BarrelRoll),
        typeof(HideAndSneak),
        typeof(TrickDaggerCard),
        typeof(RunAndGun),
        typeof(StepAway),
        typeof(Patience)
    ];
    private static List<Type> Scarlet_Uncommon_Card_Types = [
        /* Scarlet's uncommon cards. */
        typeof(BlinkStrike),
        typeof(CutTheEngines),
        typeof(Afterburn),
        typeof(UncannyDodge),
        typeof(SangeDaggerCard),
        typeof(Backstab),
        typeof(YashaDaggerCard),
        typeof(FlankingManeuver),
        typeof(DefensivePiloting)
    ];
    private static List<Type> Scarlet_Rare_Card_Types = [
        /* Scarlet's rare cards. */
        typeof(FadeCard),
        typeof(AileronRoll),
        typeof(Vendetta),
        typeof(TricksOfTheTradeRemastered),
        typeof(BulletHell),
        typeof(Reevaluate)
    ];
    private static List<Type> Scarlet_Special_Card_Types = [
        /* Scarlet's special cards. */
        typeof(ScarletEXE) //Cat's EXE card for Scarlet.
    ];
        /* Concat all Scarlet cards. */
    private static IEnumerable<Type> Scarlet_All_Card_Types = 
        Scarlet_Common_Card_Types
            .Concat(Scarlet_Uncommon_Card_Types)
            .Concat(Scarlet_Rare_Card_Types)
            .Concat(Scarlet_Special_Card_Types);
    private static List<Type> Scarlet_Common_Artifact_Types = [
        /* Scarlet's common artifacts. */
        typeof(ElectrolyteSurge),
        typeof(TrickAction),
        typeof(CardEngine)
    ];
    private static List<Type> Scarlet_Boss_Artifact_Types = [
        /* Scarlet's boss artifacts. */
        typeof(CloakAndDagger),
        typeof(ReactionWheel),
        typeof(HardlightAfterburners),
        typeof(Twinsticks),
        typeof(EngagementRings)
    ];
        /* Concat all Scarlet artifacts. */
    private static IEnumerable<Type> Scarlet_All_Artifact_Types =
        Scarlet_Common_Artifact_Types
            .Concat(Scarlet_Boss_Artifact_Types);
    private static IEnumerable<Type> Scarlet_Content =
        Scarlet_All_Card_Types
            .Concat(Scarlet_All_Artifact_Types);
    /* Scarlet Content */
    /* T2XS Vanguard Content */
    internal IShipEntry Vanguard_Ship { get; }
    /* T2XS Vanguard Content */
    /* Concat everything for registration. */
    private static IEnumerable<Type> AllRegisterableTypes =
        Vionheart_Content
            .Concat(Scarlet_Content);
    public VionheartScarlet(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {

        Instance = this;
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2; //Updated to V2!
        Harmony = new Harmony("VionheartScarlet"); //New API? (Harmony)
        MoreDifficultiesApi = helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties", (SemanticVersion?)null);
        DuoArtifactsApi = helper.ModRegistry.GetApi<IDuoArtifactsApi>("Shockah.DuoArtifacts");
        modDialogueInited = false;
        UniqueName = package.Manifest.UniqueName;
        /* Urufudoggo's new Dialogue Machine */
        helper.Events.OnModLoadPhaseFinished += (_, phase) =>
        {
            if (phase == ModLoadPhase.AfterDbInit)
            {
                localDB = new(helper, package);
            }
        };
        helper.Events.OnLoadStringsForLocale += (_, thing) =>
        {
            foreach (KeyValuePair<string, string> entry in localDB.GetLocalizationResults())
            {
                thing.Localizations[entry.Key] = entry.Value;
            }
        };
        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );
        /* Urufudoggo's new Dialogue Machine */
        /* Scarlet Content */
            /* Assign Deck */
        Scarlet_Deck = helper.Content.Decks.RegisterDeck("ScarletDeck", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                color = new Color("BC2C3D"), //old color: 560319
                titleColor = new Color("FFFFFF")
            },
            DefaultCardArt = RegisterSprite(package, "assets/cards/cardbg_scarlet.png").Sprite,
            BorderSprite = RegisterSprite(package, "assets/cards/border_scarlet.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "Scarlet", "name"]).Localize
        }
        );
            /* Sprites */
                /* Scarlet NEUTRAL */
        RegisterAnimation(package, "neutral", "assets/characters/scarlet_neutral_", 4);
                /* Scarlet GAMEOVER */
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Scarlet_Deck.Deck.Key(),
            LoopTag = "gameover",
            Frames = [
                RegisterSprite(package, "assets/characters/scarlet_gameover_3.png").Sprite,
            ]
        }
        );
                /* Scarlet MINI */
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Scarlet_Deck.Deck.Key(),
            LoopTag = "mini",
            Frames = [
                RegisterSprite(package, "assets/characters/scarlet_mini_1.png").Sprite,
            ]
        }
        );
                /* Scarlet SQUINT */
        RegisterAnimation(package, "squint", "assets/characters/scarlet_squint2_", 5);
                /* Scarlet SAD */
        RegisterAnimation(package, "sad", "assets/characters/scarlet_sad_", 5);
                /* Scarlet SMUG */
        RegisterAnimation(package, "smug", "assets/characters/scarlet_smug_", 4);
                /* Scarlet ANGRY */
        RegisterAnimation(package, "angry", "assets/characters/scarlet_angry_", 5);
                /* Scarlet TIRED */
        RegisterAnimation(package, "tired", "assets/characters/scarlet_tired_", 5);
                /* Scarlet HAPPY */
        RegisterAnimation(package, "happy", "assets/characters/scarlet_happy_", 4);
                /* Scarlet FLUSTERED */
        RegisterAnimation(package, "flustered", "assets/characters/scarlet_flustered_", 4);
                /* Scarlet BLUSH */
        RegisterAnimation(package, "blush", "assets/characters/scarlet_blush_", 4);
                /* Scarlet DAGGER */
        RegisterAnimation(package, "dagger", "assets/characters/scarlet_dagger_", 4);
                /* Scarlet DAGGERANGRY */
        RegisterAnimation(package, "daggerangry", "assets/characters/scarlet_daggerangry_", 5);
                /* Scarlet FOCUS */
        RegisterAnimation(package, "focus", "assets/characters/scarlet_focus_", 5);
                /* Scarlet LOCKEDIN */
        RegisterAnimation(package, "lockedin", "assets/characters/scarlet_lockedin_", 5);
                /* Scarlet MISCHIEF */
        RegisterAnimation(package, "mischief", "assets/characters/scarlet_mischief_", 5);
                /* Scarlet DAGGERTAUNT */
        RegisterAnimation(package, "daggertaunt", "assets/characters/scarlet_daggertaunt_", 4);
                /* Scarlet SCREAM */
        RegisterAnimation(package, "scream", "assets/characters/scarlet_scream_", 2);
                /* Scarlet NERVOUS */
        RegisterAnimation(package, "nervous", "assets/characters/scarlet_nervous_", 4);
            /* Register Scarlet as a Playable Character plus his Deck */
        helper.Content.Characters.V2.RegisterPlayableCharacter("Scarlet", new PlayableCharacterConfigurationV2
        {
            Deck = Scarlet_Deck.Deck,
            BorderSprite = RegisterSprite(package, "assets/characters/char_scarlet.png").Sprite,
            Starters = new StarterDeck
            {
                cards = [
                    new HideAndSneak(),
                    new SneakAttack()
                ],
                artifacts = [
                ]
            },
            Description = AnyLocalizations.Bind(["character", "Scarlet", "description"]).Localize,
            ExeCardType = typeof(ScarletEXE),
            SoloStarters = new StarterDeck
            {
                cards = [
                    new HideAndSneak(),
                    new SneakAttack(),
                    new AdjustThrottle(),
                    new RunAndGun(),
                    new CannonColorless(),
                    new BasicShieldColorless()
                ]
            }
        }
        );
        /* MoreDifficulties mod - Scarlet's Alternate Starter Cards */
        if (MoreDifficultiesApi != null)
	    {
            Deck Deck = Scarlet_Deck.Deck;
            StarterDeck altStarters = new StarterDeck
            {
                cards = [
                    new TrickDaggerCard(),
                    new RunAndGun()
                ],
                artifacts = [
                ]
            };
            MoreDifficultiesApi.RegisterAltStarters(Deck, altStarters);
	    }
        /* MoreDifficulties mod - Scarlet's Alternate Starter Cards */
        /* Fade status */
        Fade = helper.Content.Statuses.RegisterStatus("Fade", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = true,
                color = new Color("BC2C3D"),
                icon = RegisterSprite(package, "assets/icons/Fade.png").Sprite
            },
            Name = AnyLocalizations.Bind(["status", "Fade", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Fade", "description"]).Localize
        }
        );
        _ = new FadeManager(package, helper);
        /* Scarlet Barrage status */
        scarletBarrage = helper.Content.Statuses.RegisterStatus("Scarlet Barrage", new StatusConfiguration
        {
            Name = AnyLocalizations.Bind(["status", "scarletBarrage", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "scarletBarrage", "description"]).Localize,
            Definition = new StatusDef
            {
                isGood = true,
                affectedByTimestop = true,
                color = new Color("BC2C3D"),
                icon = RegisterSprite(package, "assets/icons/Scarlet-Barrage.png").Sprite
            }
        }
        );
        _ = new scarletBarrageManager(package, helper);
        /* Saturation status */
        Saturation = helper.Content.Statuses.RegisterStatus("Saturation", new StatusConfiguration
        {
            Name = AnyLocalizations.Bind(["status", "Saturation", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Saturation", "description"]).Localize,
            Definition = new StatusDef
            {
                isGood = false,
                affectedByTimestop = false,
                color = new Color("BC2C3D"),
                icon = RegisterSprite(package, "assets/icons/Saturation.png").Sprite
            },
            ShouldFlash = (State state, Combat combat, Ship ship, Status status) =>
            {
                return true;
            }
        } 
        );
        _ = new SaturationManager(package, helper);
        /* Trick Dagger midrow + icon */
        TrickDagger = RegisterSprite(package, "assets/midrow/Trick-Dagger.png");
        TrickDagger_Icon = RegisterSprite(package, "assets/icons/Trick-Dagger_Icon.png");
        TrickDagger_Seeker_Animated = RegisterSprite(package, "assets/midrow/Trick-Dagger_Seeker.gif");
        TrickDagger_Seeker = RegisterSprite(package, "assets/midrow/Trick-Dagger_Seeker_0.png");
        TrickDagger_Seeker_Angled = RegisterSprite(package, "assets/midrow/Trick-Dagger_Seeker_1.png");
        TrickDagger_Seeker_Icon = RegisterSprite(package, "assets/icons/Trick-Dagger_Seeker_Icon.png");
        TrickDagger_Heavy = RegisterSprite(package, "assets/midrow/Trick-Dagger_Heavy.png");
        TrickDagger_Heavy_Icon = RegisterSprite(package, "assets/icons/Trick-Dagger_Heavy_Icon.png");
        /* Trick Dagger Seeker midrow + icon */
        /* T2XS Vanguard */
        var Vanguard_Wing = helper.Content.Ships.RegisterPart("Vanguard.Wing", new PartConfiguration()
        {
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_engine-1.png")).Sprite
        }
        );
        var Vanguard_Cockpit = helper.Content.Ships.RegisterPart("Vanguard.Cockpit", new PartConfiguration()
        {
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_cockpit-1.png")).Sprite
        }
        );
        var Vanguard_Cannon = helper.Content.Ships.RegisterPart("Vanguard.Cannon", new PartConfiguration()
        {
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_cannon-1.png")).Sprite
        }
        );
        var Vanguard_Missile = helper.Content.Ships.RegisterPart("Vanguard.Missile", new PartConfiguration()
        {
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_missiles-1.png")).Sprite
        }
        );
        var Vanguard_Extra_1 = helper.Content.Ships.RegisterPart("Vanguard.Extra-1", new PartConfiguration()
        {
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_engine-2.png")).Sprite
        }
        );
        var Vanguard_Extra_2 = helper.Content.Ships.RegisterPart("Vanguard.Extra-2", new PartConfiguration()
        {
            Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_cockpit-2.png")).Sprite
        }
        );
        var Vanguard_Chassis = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/ships/vanguard_chassis-1.png")).Sprite;
        Vanguard_Ship = helper.Content.Ships.RegisterShip("Vanguard", new ShipConfiguration()
        {
            Ship = new StarterShip()
            {
                ship = new Ship()
                {
                    hull = 18,
                    hullMax = 18,
                    shieldMaxBase = 6,
                    parts =
                    {
                        new Part
                        {
                            type = PType.wing,
                            skin = Vanguard_Wing.UniqueName
                        },
                        new Part
                        {
                            type = PType.wing,
                            skin = Vanguard_Extra_1.UniqueName,
                            damageModifier = PDamMod.armor
                        },
                        new Part
                        {
                            type = PType.missiles,
                            skin = Vanguard_Missile.UniqueName
                        },
                        new Part
                        {
                            type = PType.cockpit,
                            skin = Vanguard_Cockpit.UniqueName
                        },
                        new Part
                        {
                            type = PType.cannon,
                            skin = Vanguard_Cannon.UniqueName
                        },
                        new Part
                        {
                            type = PType.wing,
                            skin = Vanguard_Extra_1.UniqueName,
                            damageModifier = PDamMod.armor,
                            flip = true
                        },
                        new Part
                        {
                            type = PType.wing,
                            skin = Vanguard_Wing.UniqueName,
                            flip = true
                        }
                    }
                },
                cards =
                {
                    new BasicShieldColorless(),
                    new CannonColorless(),
                    new CannonColorless(),
                    new DodgeColorless()
                },
                artifacts =
                {
                    new ShieldPrep(),
                    new VanguardBerthing()
                }
            },
            ExclusiveArtifactTypes = new HashSet<Type>()
            {
                typeof(VanguardBerthing),
                typeof(VanguardBerthingInitial),
                typeof(VanguardBerthingPlus)
            },
            UnderChassisSprite = Vanguard_Chassis,
            Name = AnyLocalizations.Bind(["ship", "Vanguard", "name"]).Localize,
            Description = AnyLocalizations.Bind(["ship", "Vanguard", "description"]).Localize
        }
        );
        /* T2XS Vanguard */
        // /* Vanguard Boss */
        // Vanguard_Ship = helper.Content.Ships.RegisterShip("VanguardBoss", new ShipConfiguration()
        // {
        //     Ship = new StarterShip()
        //     {
        //         ship = new Ship()
        //         {
        //             hull = 18,
        //             hullMax = 18,
        //             shieldMaxBase = 6,
        //             parts =
        //             {
        //                 new Part
        //                 {
        //                     type = PType.wing,
        //                     skin = Vanguard_Extra_1.UniqueName,
        //                     damageModifier = PDamMod.armor
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.cannon,
        //                     skin = Vanguard_Cannon.UniqueName
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.wing,
        //                     skin = Vanguard_Extra_1.UniqueName,
        //                     damageModifier = PDamMod.armor,
        //                     flip = true
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.missiles,
        //                     skin = Vanguard_Missile.UniqueName
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.cockpit,
        //                     skin = Vanguard_Cockpit.UniqueName
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.cannon,
        //                     skin = Vanguard_Cannon.UniqueName
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.wing,
        //                     skin = Vanguard_Extra_1.UniqueName,
        //                     damageModifier = PDamMod.armor,
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.missiles,
        //                     skin = Vanguard_Missile.UniqueName
        //                 },
        //                 new Part
        //                 {
        //                     type = PType.wing,
        //                     skin = Vanguard_Extra_1.UniqueName,
        //                     damageModifier = PDamMod.armor,
        //                     flip = true
        //                 },
        //             }
        //         },
        //         cards =
        //         {
        //             new BasicShieldColorless(),
        //             new CannonColorless(),
        //             new CannonColorless(),
        //             new DodgeColorless()
        //         },
        //         artifacts =
        //         {
        //             new ShieldPrep()
        //         }
        //     },
        //     ExclusiveArtifactTypes = new HashSet<Type>()
        //     {
        //     },
        //     UnderChassisSprite = Vanguard_Chassis,
        //     Name = AnyLocalizations.Bind(["ship", "VanguardBoss", "name"]).Localize,
        //     Description = AnyLocalizations.Bind(["ship", "VanguardBoss", "description"]).Localize
        // }
        // );
        // /* Vanguard Boss */
        /* HullLostManager */
        _ = new HullLostManager(package, helper);
        /* HullLostManager */
        /* CheckIfAttackIsPiercing */
        _ = new CheckIfAttackIsPiercing(package, helper);
        /* CheckIfAttackIsPiercing */
        /* Register all artifacts and cards into the game, allowing it to be played. (Based on AllRegisterableTypes) */
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);
        /* Inject Dialogue */
        // Dialogue.Dialogue.Inject(); //Replaced with new Dialogue Machine
    }

    /* New function to register sprites better (New method). */
    public static ISpriteEntry RegisterSprite(IPluginPackage<IModManifest> package, string dir)
    {
        return Instance.Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile(dir));
    }
    /* New function to register animations better (New method). */
    public static void RegisterAnimation(IPluginPackage<IModManifest> package, string tag, string dir, int frames)
    {
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Instance.Scarlet_Deck.Deck.Key(),
            LoopTag = tag,
            Frames = Enumerable.Range(0, frames)
                .Select(i => RegisterSprite(package, dir + i + ".png").Sprite)
                .ToImmutableList()
        }
        );
    }
}