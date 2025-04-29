using System.Linq;
using VionheartScarlet;
using Nickel;

namespace VionheartScarlet.Dialogue;

internal static class CommonDefinitions
{
	internal static VionheartScarlet Instance => VionheartScarlet.Instance;
	internal static string Scarlet => VionheartScarlet.Instance.Scarlet_Deck.UniqueName;
	internal static Deck Scarlet_Deck => Instance.Scarlet_Deck.Deck;
	internal static string Eunice => Deck.eunice.Key();
	internal static Deck Eunice_Deck => Deck.eunice;
	internal static string Riggs => Deck.riggs.Key();
	internal static string Cat => "comp";
	internal static string EvilRiggs => "pirateBoss";
	internal static string Brimford => "walrus";
	// internal static string AmCraig = "craig";

	// internal const string AmUnknown = "johndoe";

	// internal const string AmCat = "comp";

	// internal const string AmVoid = "void";

	// internal const string AmShopkeeper = "nerd";

	// internal static VionheartScarlet Instance => VionheartScarlet.Instance;

	// internal static string AmIScarlet => VionheartScarlet.Instance.Scarlet_Deck.UniqueName;

	// internal static Deck AmIScarletDeck => Instance.Scarlet_Deck.Deck;

	// internal static string AmDizzy => EnumExtensions.Key((Deck)1);

	// internal static string AmPeri => EnumExtensions.Key((Deck)3);

	// internal static string AmRiggs => EnumExtensions.Key((Deck)2);

	// internal static string AmDrake => EnumExtensions.Key((Deck)5);

	// internal static string AmIssac => EnumExtensions.Key((Deck)4);

	// internal static string AmBooks => EnumExtensions.Key((Deck)7);

	// internal static string AmMax => EnumExtensions.Key((Deck)6);

	// //internal static Status MissingScarlet => VionheartScarlet.Scarlet.MissingStatus.Status;

	// // internal static string Check(this string loopTag)
	// // {
	// // 	if (VionheartScarlet.Instance.Helper.Content.Contains(loopTag))
	// // 	{
	// // 		return loopTag;
	// // 	}
	// // 	return "neutral";
	// // }

	// // internal static string F(this string Name)
	// // {
	// // 	return Instance.UniqueName + "::" + Name;
	// // }
}