using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//The way this will work: Every character has a list of squares needed to reveeal it
//This will check those sqaures and see if they are revealed
public class HintData
{
    private static HintData Instance;

    private HintData() { }

    public static HintData GetHintData()
    {
        return Instance ??= new HintData();
    }

    List<Hint> hintsToGive = new();

    public void CheckForHints(Dictionary<CharacterClickInteractor, CharacterData> locationsToCharacters)
    {
        hintsToGive.Clear();
        HighlightHandler.GetHighlightHandler().RemoveHighlightsAndHints();
        foreach (CharacterClickInteractor location in locationsToCharacters.Keys.Where(x => !x.revealedAlready))
        {
            CheckSingleCharacter(locationsToCharacters, locationsToCharacters[location]);
            if (hintsToGive.Count > 0)
                break;
        }

        if (hintsToGive.Count < 1)
            return;


        MakeHintsAppear();
    }

    private void MakeHintsAppear()
    {
        foreach (Hint hint in hintsToGive)
        {
            foreach (Vector2Int gridLocation in hint.neededAndHighlight)
            {
                CharacterClickInteractor character = GridManager.GetGridManager().GetCharacterAtPosition(gridLocation.x - 1, gridLocation.y - 1);
                HighlightHandler.GetHighlightHandler().AddObjectToHints(character);
            }
        }
    }

    private void CheckSingleCharacter(Dictionary<CharacterClickInteractor, CharacterData> locationsToCharacters, CharacterData character)
    {
        foreach (Hint hint in character.hint)
        {
            bool thisHintWorks = true;
            foreach (Vector2Int ints in hint.neededWithoutHighlight)
            {
                if (!GridManager.GetGridManager().GetCharacterAtPosition(ints.x-1, ints.y-1).revealedAlready)
                {
                    thisHintWorks = false;
                }
            }

            foreach (Vector2Int ints in hint.neededAndHighlight)
            {
                if (!GridManager.GetGridManager().GetCharacterAtPosition(ints.x-1, ints.y-1).revealedAlready)
                {
                    thisHintWorks = false;
                }
            }

            if (thisHintWorks)
            {
                hintsToGive.Add(hint);
            }
        }
    }
}

//IDEA: When you click "Hint", it should look through all hints for sections not already unlocked and reveal the first valid hint it findes (hint for an unreveled slot
//that has all needed hints revealed)

//#1: mMybe just keep a list of all character spots, then go through that list until you find one that is not revealed, check it's hints,
//    and keep going if they are not good

//#2: Keep a list of valid hints. When a hint is no longer valid, remove it. (How would i remove it? A dictionary that removes every value at a given key?) 