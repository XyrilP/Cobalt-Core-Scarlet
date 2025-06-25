using System;

namespace VionheartScarlet.ExternalAPI;

public interface IEssentialsApi
{
    Type? GetExeCardTypeForDeck(Deck deck);
    bool IsBlacklistedExeOffering(Deck obj);
    bool IsExeCardType(Type type);
}