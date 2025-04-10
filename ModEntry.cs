using Nickel;
using HarmonyLib;
//using AuthorName.DemoMod.Cards;
//using AuthorName.DemoMod.Artifacts;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Linq;
using XyrilP.VionheartScarlet.Cards;



namespace XyrilP.VionheartScarlet;
public sealed class VionheartScarlet : SimpleMod
{

    internal static VionheartScarlet Instance { get; private set; } = null!;
    internal IKokoroApi KokoroApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    /* Declare stuff! */
    
        /* Sprites */
    internal ISpriteEntry Scarlet_Character_Panel { get; }
    internal ISpriteEntry Scarlet_Character_Mini { get; }
    internal ISpriteEntry Scarlet_Character_Neutral_1 { get; }
    internal ISpriteEntry Scarlet_Character_Neutral_2 { get; }
    internal ISpriteEntry Scarlet_Character_Neutral_3 { get; }
    internal ISpriteEntry Scarlet_Character_Neutral_4 { get; }
    internal ISpriteEntry Scarlet_Character_Gameover { get; }
    internal IDeckEntry Scarlet_Deck { get; }
    internal ISpriteEntry Scarlet_Character_CardBackground { get; }
    internal ISpriteEntry Scarlet_Character_CardFrame { get; }
    internal IStatusEntry ScarletFade { get; }

        /* Scarlet's cards */
    internal static IReadOnlyList<Type> Scarlet_StarterCard_Types { get; } = [
        /* Scarlet's starter cards. */
        typeof(ScarletVeer),
        typeof(ScarletDriveBy)
    ];
    internal static IReadOnlyList<Type> Scarlet_CommonCardTypes { get; } = [
        /* Scarlet's common cards. */
        typeof(ScarletVeer),
        typeof(ScarletDriveBy),
        typeof(ScarletThrottleDown),
        typeof(ScarletThrottleUp),
        typeof(ScarletSneakAttack)
    ];
    internal static IReadOnlyList<Type> Scarlet_UncommonCardTypes { get; } = [
        /* Scarlet's uncommon cards. */
        typeof(ScarletArtemisMissile),
        typeof(ScarletDriftMissile),
        typeof(ScarletBarrelRoll),
        typeof(ScarletBlinkStrike),
        typeof(ScarletTricksOfTheTrade)
    ];
    internal static IReadOnlyList<Type> Scarlet_RareCardTypes { get; } = [
        /* Scarlet's rare cards. */
        typeof(ScarletFadeCard),
        typeof(ScarletAileronRoll),
        typeof(ScarletVendetta)
    ];

    /* Concat all Scarlet cards. */
    internal static IEnumerable<Type> Scarlet_AllCard_Types
    => Scarlet_StarterCard_Types
    .Concat(Scarlet_CommonCardTypes)
    .Concat(Scarlet_UncommonCardTypes)
    .Concat(Scarlet_RareCardTypes);

    internal static IReadOnlyList<Type> Scarlet_CommonArtifact_Types { get; } = [
        /* Scarlet's common artifacts. */
    ];


    public VionheartScarlet(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {

        Instance = this;

        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;

        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        /* Assign sprites */
        Scarlet_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/char_scarlet.png"));
        Scarlet_Character_Mini = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/scarlet_mini.png"));
        Scarlet_Character_Neutral_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/scarlet_neutral_1.png"));
        Scarlet_Character_Neutral_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/scarlet_neutral_2.png"));
        Scarlet_Character_Neutral_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/scarlet_neutral_3.png"));
        Scarlet_Character_Neutral_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/scarlet_neutral_4.png"));
        Scarlet_Character_Gameover = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/scarlet_gameover.png"));
        Scarlet_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/cardbg_scarlet.png"));
        Scarlet_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/border_scarlet.png"));

        /* Assign decks */
        Scarlet_Deck = helper.Content.Decks.RegisterDeck("ScarletDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("560319"),
                titleColor = new Color("FFFFFF")
            },
            DefaultCardArt = Scarlet_Character_CardBackground.Sprite,
            BorderSprite = Scarlet_Character_CardFrame.Sprite,
            Name = AnyLocalizations.Bind(["character", "Scarlet", "name"]).Localize,
        }
        );

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration(){

            Deck = Scarlet_Deck.Deck,

            LoopTag = "neutral",
            Frames = new[]
            {
                Scarlet_Character_Neutral_1.Sprite,
                Scarlet_Character_Neutral_2.Sprite,
                Scarlet_Character_Neutral_3.Sprite,
                Scarlet_Character_Neutral_4.Sprite,
            }

        }
        );

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration(){

            Deck = Scarlet_Deck.Deck,

            LoopTag = "gameover",
            Frames = new[]
            {
                Scarlet_Character_Gameover.Sprite
            }

        }
        );

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration(){
        
            Deck = Scarlet_Deck.Deck,
            LoopTag = "mini",
            Frames = new[]
            {
                Scarlet_Character_Mini.Sprite
            }

        }
        );

        helper.Content.Characters.RegisterCharacter("Scarlet", new CharacterConfiguration(){

            Deck = Scarlet_Deck.Deck,
            StarterCardTypes = Scarlet_StarterCard_Types,
            Description = AnyLocalizations.Bind(["character", "Scarlet", "description"]).Localize,
            BorderSprite = Scarlet_Character_Panel.Sprite

        }
        );

        foreach (var cardType in Scarlet_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IScarletCard.Register))?.Invoke(null, [helper]);

        ScarletFade = helper.Content.Statuses.RegisterStatus("ScarletFade", new()
        {

            Definition = new()
            {

                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/scarletFade.png")).Sprite,
                color = new("FFFFFF"),
                isGood = true

            },

            Name = AnyLocalizations.Bind(["status", "ScarletFade", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "ScarletFade", "description"]).Localize

        });

        _ = new ScarletFade();

        

    }


}