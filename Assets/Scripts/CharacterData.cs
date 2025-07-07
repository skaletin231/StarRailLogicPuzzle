using System;
using UnityEngine;

[CreateAssetMenu(menuName = "New Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    [TextArea(5,7)]
    public string hintText;
    public string[] acceptableAnswers;
    public Hint[] hint;
    [NonSerialized] public CharacterClickInteractor myLocation = null;

    public void SetAcceptableAnswers()
    {
        if (acceptableAnswers.Length < 1)
        {
            acceptableAnswers = new string[1];
            acceptableAnswers[0] = characterName;
        }
    }
}

[Serializable]
public class Hint
{
    public Vector2Int[] neededAndHighlight;
    public Vector2Int[] neededWithoutHighlight;
}