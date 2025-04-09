using Nickel;
using HarmonyLib;
//using AuthorName.DemoMod.Cards;
//using AuthorName.DemoMod.Artifacts;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Linq;



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
    internal IDeckEntry Scarlet_Deck { get; }
    internal ISpriteEntry Scarlet_Character_CardBackground { get; }
    internal ISpriteEntry Scarlet_Character_CardFrame { get; }

        /* Scarlet's cards */
    internal static IReadOnlyList<Type> Scarlet_StarterCard_Types { get; } = [
        /* Scarlet's starter cards. */
    ];
    internal static IReadOnlyList<Type> Scarlet_CommonCardTypes { get; } = [
        /* Scarlet's common cards. */
    ];
    internal static IReadOnlyList<Type> Scarlet_UncommonCardTypes { get; } = [
        /* Scarlet's uncommon cards. */
    ];
    internal static IReadOnlyList<Type> Scarlet_RareCardTypes { get; } = [
        /* Scarlet's rare cards. */
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

        /* Assign decks */
        Scarlet_Deck = helper.Content.Decks.RegisterDeck("ScarletDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("560319"),
                titleColor = new Color("000000")
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
        
            Scarlet_Deck = Scarlet_Deck.Deck,
            LoopTag = "mini",
            Frames = new[]
            {
                Scarlet_Character_Mini.Sprite
            }

        }
        );

        helper.Content.Characters.RegisterCharacter("Scarlet", new CharacterConfiguration(){

            Deck = Scarlet_Deck.Deck,
            Scarlet_StarterCard_Types = Scarlet_StarterCard_Types,
            Description = AnyLocalizations.Bind(["character", "Scarlet", "description"]).Localize,
            BorderSprite = Scarlet_Character_Panel.Sprite

        }
        );

    }


}