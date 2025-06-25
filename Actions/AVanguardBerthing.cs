using System.Collections.Generic;
using System.Linq;
using daisyowl.text;
using VionheartScarlet.ExternalAPI;

namespace VionheartScarlet.Actions;

public class AVanguardBerthing : CardAction
{
    public required int choiceAmount;
    public bool upgradedStarters;
    public int addCommonCards;
    public int addUncommonCards;
    public int addRareCards;

    public override Route? BeginWithRoute(G g, State s, Combat c)
    {
        List<Deck> choices = s.storyVars.GetUnlockedChars().Shuffle(s.rngActions).Except(
            s.characters.Where(character => character.deckType != null).Select(character => character.deckType!.Value)
        ).ToList();
        if (VionheartScarlet.Instance.EssentialsApi != null)
        {
            choices.RemoveAll(VionheartScarlet.Instance.EssentialsApi.IsBlacklistedExeOffering);
        }
        if (VionheartScarlet.Instance.MoreDifficultiesApi != null)
        {
            choices.RemoveAll(deck => VionheartScarlet.Instance.MoreDifficultiesApi.IsBanned(s, deck));
        }
        choices = choices.Take(choiceAmount).ToList();
        return new ActionRoute
        {
            choices = choices,
            upgradedStarters = upgradedStarters,
            addCommonCards = addCommonCards,
            addUncommonCards = addUncommonCards,
            addRareCards = addRareCards
        };
    }
}
public class ActionRoute : Route, OnMouseDown
{
    public required List<Deck> choices;
    public bool upgradedStarters;
    public int addCommonCards;
    public int addUncommonCards;
    public int addRareCards;
    public override bool GetShowOverworldPanels()
        => true;
    public override bool CanBePeeked()
        => true;
    public override void Render(G g)
    {
        base.Render(g);
        SharedArt.DrawEngineering(g);
        Draw.Text(VionheartScarlet.Instance.Localizations.Localize(["artifact","VanguardBerthingChoice", "title"]), G.screenSize.x / 2, 48, DB.pinch, Colors.textMain, align: TAlign.Center);
        Draw.Text(VionheartScarlet.Instance.Localizations.Localize(["artifact","VanguardBerthingChoice", "choice"]), G.screenSize.x / 2, 90, DB.pinch, Colors.textFaint, align: TAlign.Center);
        for (int i = 0; i < choices.Count; i++)
        {
            Deck deck = choices[i];
            Character character = new()
            {
                type = deck.Key(),
                deckType = deck
            };
            character.Render(g, (int)(208.0 + 64.0 * (i - (choices.Count - 1) / 2.0)), 104, onMouseDown: this);
        }
    }
    public void OnMouseDown(G g, Box b)
    {
        State s = g.state;
        if (b.key == null)
        {
            return;
        }
        else if (b.key.Value.k == StableUK.character)
        {
            Deck deck = (Deck)b.key.Value.v;
            List<CardAction> actions =
            [
                new AAddCharacter
                    {
                        deck = deck,
                        canGoPastTheCharacterLimit = true,
                        timer = 0
                    }
            ];
            StarterDeck starters = (StarterDeck)((VionheartScarlet.Instance.MoreDifficultiesApi != null && VionheartScarlet.Instance.MoreDifficultiesApi.HasAltStarters(deck) && g.state.rngActions.NextInt() % 2 == 0) ? VionheartScarlet.Instance.MoreDifficultiesApi.GetAltStarters(deck)! : StarterDeck.starterSets[deck]);
            actions.AddRange(starters.cards.Select(card =>
            {
                Card newCard = card.CopyWithNewId();
                if (upgradedStarters)
                {
                    var upgradesTo = newCard.GetMeta().upgradesTo;
                    newCard.upgrade = upgradesTo[g.state.rngActions.NextInt() % upgradesTo.Length];
                }
                return new AAddCard
                {
                    card = newCard,
                    timer = 0,
                    destination = CardDestination.Deck,
                    callItTheDeckNotTheDrawPile = true
                };
            }
            )
            );
            actions.AddRange(starters.artifacts.Select(artifact => new AAddArtifact
            {
                artifact = Mutil.DeepCopy(artifact),
                timer = 0,
            }
            )
            );
            actions.Add(new ADummyAction
            {
                dialogueSelector = deck.Key() + "_VanguardBerthingAdd",
                timer = 0
            }
            );
            if (addCommonCards > 0)
            {
                for (int i = 0; i < addCommonCards; i++)
                {
                    actions.Add(new ACardOffering
                    {
                        amount = 3,
                        limitDeck = deck,
                        canSkip = true,
                        rarityOverride = Rarity.common
                    }
                    );
                }
            }
            if (addUncommonCards > 0)
            {
                for (int i = 0; i < addUncommonCards; i++)
                {
                    actions.Add(new ACardOffering
                    {
                        amount = 3,
                        limitDeck = deck,
                        canSkip = true,
                        rarityOverride = Rarity.uncommon
                    }
                    );
                }
            }
            if (addRareCards > 0)
            {
                for (int i = 0; i < addRareCards; i++)
                {
                    actions.Add(new ACardOffering
                    {
                        amount = 3,
                        limitDeck = deck,
                        canSkip = true,
                        rarityOverride = Rarity.rare
                    }
                    );
                }
            }
            s.GetCurrentQueue().AddRange(actions);
            g.CloseRoute(this);
        }
    }
}