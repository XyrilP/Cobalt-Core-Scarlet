using System.Linq;
using VionheartScarlet;
using Nickel;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace VionheartScarlet.Dialogue;

internal static class CommonDefinitions
{
    internal static VionheartScarlet Instance => VionheartScarlet.Instance;
    internal static string Scarlet => Instance.Scarlet_Deck.UniqueName;
    internal static Deck Scarlet_Deck => Instance.Scarlet_Deck.Deck;
    internal static Status Fade => Instance.Fade.Status;
    internal static Status SaturationBarrage => Instance.SaturationBarrage.Status;
    internal static Status Missing_Scarlet => Instance.Scarlet.MissingStatus.Status;
    internal static string Dizzy => Deck.dizzy.Key();
    internal static Deck Dizzy_Deck => Deck.dizzy;
    internal static string Riggs => Deck.riggs.Key();
    internal static Deck Riggs_Deck => Deck.riggs;
    internal static string Hyperia => Deck.peri.Key();
    internal static Deck Hyperia_Deck => Deck.peri;
    internal static string Isaac => Deck.goat.Key();
    internal static Deck Isaac_Deck => Deck.goat;
    internal static string Max => Deck.hacker.Key();
    internal static Deck Max_Deck => Deck.hacker;
    internal static string Eunice => Deck.eunice.Key();
    internal static Deck Eunice_Deck => Deck.eunice;
    internal static string Books => Deck.shard.Key();
    internal static Deck Books_Deck => Deck.shard;
    internal static string Cat => Deck.colorless.Key();
    internal static Deck Cat_Deck => Deck.colorless;
    internal static string EvilRiggs => "pirateBoss";
    internal static string Brimford => "walrus";
    internal static string Triune => "void";
    internal static string Ratzo => "knight";
    internal static string Wizbo => "wizard";
    internal static string Cleo => "nerd";
    internal static string Illeana = "urufudoggo.Illeana::illeana";
    internal static string Ruhig = "havmir.RuhigMod::Demo";
    internal static string Weth = "urufudoggo.Weth::weth";
    internal static Deck TryGetDeck(this string who)
    {
        IDeckEntry who_Deck = VionheartScarlet.Instance.Helper.Content.Decks.LookupByUniqueName(who)!;
        if (who_Deck is not null) return who_Deck.Deck;
        else return Deck.colorless;
    }
    internal static Status TryGetMissing(this string who)
    {
        if (who is not null && VionheartScarlet.Instance.Helper.Content.Characters.V2.LookupByUniqueName(who) is IPlayableCharacterEntryV2 entry)
        {
            return entry.MissingStatus.Status;
        }
        return Missing_Scarlet;
    }
}