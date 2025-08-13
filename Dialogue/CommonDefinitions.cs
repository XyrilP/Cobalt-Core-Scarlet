using System.Linq;
using VionheartScarlet;
using Nickel;

namespace VionheartScarlet.Dialogue;

internal static class CommonDefinitions
{
    internal static VionheartScarlet Instance => VionheartScarlet.Instance;
    internal static string Scarlet => VionheartScarlet.Instance.Scarlet_Deck.UniqueName;
    internal static Deck Scarlet_Deck => Instance.Scarlet_Deck.Deck;
    internal static Status Fade => VionheartScarlet.Instance.Fade.Status;
    internal static Status SaturationBarrage => VionheartScarlet.Instance.SaturationBarrage.Status;
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
}