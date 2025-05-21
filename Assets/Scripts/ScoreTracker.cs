using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreTracker
{
    private static ScoreTracker Instance;

    private ScoreTracker() { }

    public static ScoreTracker GetScoreTracker()
    {
        return Instance ??= new ScoreTracker();
    }

    List<CharacterClickInteractor> allCharacters = new();

    public int Score()
    {
        return allCharacters.Where(x => x.revealedAlready).Count();
    }

    public int Total()
    {
        return allCharacters.Count();
    }

    public void AddCharactersToList(List<CharacterClickInteractor> character)
    {
        allCharacters = character;
    }
}
