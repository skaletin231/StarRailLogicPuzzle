using System.Collections.Generic;

public class HighlightHandler 
{
    Stack<CharacterClickInteractor> currentlyHighlighted = new();
    Stack<CharacterClickInteractor> currentlyHinted = new();

    private static HighlightHandler Instance;

    private HighlightHandler() {}

    public static HighlightHandler GetHighlightHandler()
    {
        return Instance ??= new HighlightHandler();
    }

    //Add highlights

    public void AddObjectToHighlights(CharacterClickInteractor toHighlight)
    {
        toHighlight.HighlightMyself();
        currentlyHighlighted.Push(toHighlight);
    }
    public void AddObjectToHints(CharacterClickInteractor toHighlight)
    {
        toHighlight.HintMyself();
        currentlyHinted.Push(toHighlight);
    }

    public void HighlightJustThisObbject(CharacterClickInteractor toHighlight)
    {
        RemoveHighlights();
        AddObjectToHighlights(toHighlight);
    }



    //Remove highlights

    public void RemoveHighlightsAndHints()
    {
        RemoveHighlights();
        RemoveHints();
    }

    private void RemoveHighlights()
    {
        while (currentlyHighlighted.Count > 0)
        {
            (currentlyHighlighted.Pop()).RemoveMyHighlight();
        }
    }

    public void RemoveHints()
    {
        while (currentlyHinted.Count > 0)
        {
            (currentlyHinted.Pop()).RemoveMyHighlight();
        }
    }
}
