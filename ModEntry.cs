using Nickel;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using XyrilP.ExternalAPI;
using XyrilP.VionheartScarlet.Cards;



namespace XyrilP.VionheartScarlet;
internal class VionheartScarlet : SimpleMod
{
    /* Declare stuff! */

    internal static VionheartScarlet Instance { get; private set; } = null!;
    internal Harmony Harmony;
    internal IKokoroApi.IV2 KokoroApi;
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    internal IDeckEntry Scarlet_Deck; //Scarlet's Deck of Cards
    // internal IStatusEntry ScarletFade { get; } //Scarlet's Fade status icon

        /* Scarlet's cards */
    private static List<Type> Scarlet_CommonCardTypes = [
        /* Scarlet's common cards. */
        typeof(Veer),
        typeof(DriveBy),
        typeof(ThrottleDown),
        typeof(ThrottleUp),
        typeof(SneakAttack)
    ];
    private static List<Type> Scarlet_UncommonCardTypes = [
        /* Scarlet's uncommon cards. */
        typeof(ArtemisMissile),
        typeof(DriftMissile),
        typeof(BarrelRoll),
        typeof(BlinkStrike),
        typeof(TricksOfTheTrade)
    ];
    private static List<Type> Scarlet_RareCardTypes = [
        /* Scarlet's rare cards. */
        typeof(FadeCard),
        typeof(AileronRoll),
        typeof(Vendetta)
    ];

    /* Concat all Scarlet cards. */
    private static IEnumerable<Type> Scarlet_AllCard_Types = 
        Scarlet_CommonCardTypes
            .Concat(Scarlet_UncommonCardTypes)
            .Concat(Scarlet_RareCardTypes);

    private static List<Type> Scarlet_CommonArtifact_Types = [
        /* Scarlet's common artifacts. */
    ];

    private static List<Type> Scarlet_BossArtifact_Types = [
        /* Scarlet's boss artifacts. */
    ];

    /* Concat all Scarlet artifacts. */
    private static IEnumerable<Type> Scarlet_AllArtifact_Types =
        Scarlet_CommonArtifact_Types
            .Concat(Scarlet_BossArtifact_Types);

    /* Concat both Scarlet artifacts and cards. */
    private static IEnumerable<Type> AllRegisterableTypes =
        Scarlet_AllCard_Types
            .Concat(Scarlet_AllArtifact_Types);


    public VionheartScarlet(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {

        Instance = this;
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2; //Updated to V2!
        Harmony = new Harmony("XyrilP.VionheartScarlet"); //New API? (Harmony)

        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        /* Assign decks */
        Scarlet_Deck = helper.Content.Decks.RegisterDeck("ScarletDeck", new DeckConfiguration
        {
            Definition = new DeckDef
            {
                color = new Color("560319"),
                titleColor = new Color("FFFFFF")
            },
            DefaultCardArt = RegisterSprite(package, "assets/characters/cardbg_scarlet.png").Sprite,
            BorderSprite = RegisterSprite(package, "assets/characters/border_scarlet.png").Sprite,
            Name = AnyLocalizations.Bind(["character", "Scarlet", "name"]).Localize
        }
        );

        /* Register all artifacts and cards into the game, allowing it to be played. (Based on AllRegisterableTypes) */
        foreach (var type in AllRegisterableTypes)
            AccessTools.DeclaredMethod(type, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);

        /* Scarlet NEUTRAL */
        RegisterAnimation(package, "neutral", "assets/characters/scarlet_neutral_", 4);

        /* Scarlet GAMEOVER */
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Scarlet_Deck.Deck.Key(),
            LoopTag = "gameover",
            Frames = [
                RegisterSprite(package, "assets/characters/scarlet_gameover_0.png").Sprite,
            ]
        }
        );

        /* Scarlet MINI */
        Instance.Helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2
        {
            CharacterType = Scarlet_Deck.Deck.Key(),
            LoopTag = "mini",
            Frames = [
                RegisterSprite(package, "assets/characters/scarlet_mini_0.png").Sprite,
            ]
        }
        );

        helper.Content.Characters.V2.RegisterPlayableCharacter("Scarlet", new PlayableCharacterConfigurationV2
        {
            Deck = Scarlet_Deck.Deck,
            BorderSprite = RegisterSprite(package, "assets/characters/char_scarlet.png").Sprite,
            Starters = new StarterDeck
            {
                cards = [
                    new DriveBy(),
                    new Veer()
                ],
                artifacts = [

                ]
            },
            Description = AnyLocalizations.Bind(["character", "Scarlet", "description"]).Localize
        }
        );

        // foreach (var cardType in Scarlet_AllCard_Types)
        //     AccessTools.DeclaredMethod(cardType, nameof(IScarletCard.Register))?.Invoke(null, [helper]);

        // ScarletFade = helper.Content.Statuses.RegisterStatus("ScarletFade", new()
        // {

        //     Definition = new()
        //     {

        //         icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/scarletFade.png")).Sprite,
        //         color = new("FFFFFF"),
        //         isGood = true

        //     },

        //     Name = AnyLocalizations.Bind(["status", "ScarletFade", "name"]).Localize,
        //     Description = AnyLocalizations.Bind(["status", "ScarletFade", "description"]).Localize

        // });

        // _ = new ScarletFade();

        

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
        });
    }


}