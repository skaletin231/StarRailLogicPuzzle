using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class AddCharactersToScreen : MonoBehaviour
{
    [SerializeField] bool testingMode = false;
    [Header("Spawn Location Settings")]
    [SerializeField] RectTransform areaForSpawningCharacters;
    [SerializeField] RectTransform areaForSpawningCharactersContainer;
    [SerializeField] RectTransform areaForSpawninNumbers;
    [SerializeField] RectTransform areaForSpawninLetter;
    [SerializeField] float minWidth;

    GridLayoutGroup charactersGridLayoutGroup;
    GridLayoutGroup numbersGridLayoutGroup;
    GridLayoutGroup lettersGridLayoutGroup;

    [SerializeField] float scrollbarWidth;

    [Header("Data")]
    [SerializeField] GameObject characterPanelPrefab;
    [SerializeField] RowOfData[] rowsOfData;
    [SerializeField] GameObject numbersPrefab;

    [Header("Other")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button hintButton;


    Dictionary<CharacterClickInteractor, CharacterData> uiToCharacter = new();
    Dictionary<string, List<CharacterData>> possibleAnswersToData = new();

    HashSet<CharacterClickInteractor> notFoundYet = new();

    CharacterClickInteractor previousClickedObject = null;

    float currentHp = 0;

    private void Start()
    {
        Debug.Log(GlobalSettings.GetLifeOption());
        GridManager.ResetInstance();
        GameOverDetector.ResetInstance();
        HighlightHandler.ResetInstance();
        ScoreTracker.ResetInstance();

        charactersGridLayoutGroup = areaForSpawningCharacters.GetComponent<GridLayoutGroup>();
        numbersGridLayoutGroup = areaForSpawninNumbers.GetComponent<GridLayoutGroup>();
        lettersGridLayoutGroup = areaForSpawninLetter.GetComponent<GridLayoutGroup>();


        if (rowsOfData.Length == 0)
            return;

        inputField.onValueChanged.AddListener(CheckAnswer);
        charactersGridLayoutGroup.constraintCount = rowsOfData[0].charactersInThisRow.Length;

        GridManager.GetGridManager().CreateGrid(rowsOfData[0].charactersInThisRow.Length, rowsOfData.Length);
        //hintButton.onClick.AddListener(() => HintData.GetHintData().CheckForHints(uiToCharacter));
        hintButton.onClick.AddListener(() => HintData.GetHintData().CheckForHints());

        int i = 1;
        foreach (var row in rowsOfData)
        {
            GridManager.GetGridManager().SetEntireRow(i-1,SpawnARow(row, i));
            i++;
        }

        ScoreTracker.GetScoreTracker().AddCharactersToList(uiToCharacter.Keys.ToList());

        for (int ascii = 65; ascii < 65 + rowsOfData[0].charactersInThisRow.Length; ascii++)
        {
            GameObject text = GameObject.Instantiate(numbersPrefab, areaForSpawninLetter.transform);
            text.GetComponentInChildren<TextMeshProUGUI>().text = $"{(char)ascii}";
        }

        GameOverDetector.GetGameOverDetection().GameEnded += () =>
        {
            inputField.interactable = false;
            hintButton.interactable = false;

            inputField.text = "";
            HighlightHandler.GetHighlightHandler().EndGame();
        };

        if (GlobalSettings.GetMaxLife() >= 0) //not infinite
        {
            currentHp = GlobalSettings.GetMaxLife();
        }
        else
        {
            testingMode = true;
        }
    }

    private void Update()
    {
        if (rowsOfData.Length == 0)
            return;

        //Set wdith and height of each cell. I probably need a min width and height allowed
        float x = Mathf.Max(
            (areaForSpawningCharactersContainer.rect.width - scrollbarWidth - areaForSpawninNumbers.rect.width) / rowsOfData[0].charactersInThisRow.Length,
            minWidth
            );
        float y = Mathf.Max((GetComponent<RectTransform>().rect.height * .8f) / rowsOfData.Length, x);

        charactersGridLayoutGroup.cellSize = new Vector2(x,y);
        numbersGridLayoutGroup.cellSize = new Vector2(numbersGridLayoutGroup.cellSize.x, y);
        lettersGridLayoutGroup.cellSize = new Vector2(x, lettersGridLayoutGroup.cellSize.y);
    }

    void CheckAnswer(string newText)
    {
        if (previousClickedObject == null)
            return;

        if (previousClickedObject.revealedAlready)
            return;

        CharacterData characterBeingGuessed = uiToCharacter[previousClickedObject];
        string updatedNewText = newText.ToLower().Trim();

        //Check if this answer exists at all on anything
        if (!possibleAnswersToData.ContainsKey(updatedNewText))
        {
            return;
        }

        if (possibleAnswersToData[updatedNewText].Contains(characterBeingGuessed))
        {
            previousClickedObject.RevealPaenl();

            //This correctly tracks how many are left to find. Now i just need to use this to make a game over screen
            notFoundYet.Remove(previousClickedObject);
            HighlightHandler.GetHighlightHandler().RemoveHints();
            inputField.text = "";
            if (notFoundYet.Count == 0)
            {
                GameOverDetector.GetGameOverDetection().EndGame();
            }
        } 
        else
        {
            inputField.text = "";

            if (testingMode)
            {
                return;
            }

            currentHp -= 1;
            if (currentHp <= 0)
            {
                GameOverDetector.GetGameOverDetection().EndGame();
            }
        }
    }

    CharacterClickInteractor[] SpawnARow(RowOfData rowToSpawn, int rowIndex)
    {
        CharacterClickInteractor[] test = new CharacterClickInteractor[rowToSpawn.charactersInThisRow.Length];

        for (int columnIndex = 0; columnIndex < rowToSpawn.charactersInThisRow.Length;columnIndex++)
        {
            test[columnIndex] = SpawnASingleCharacter(rowToSpawn.charactersInThisRow[columnIndex], columnIndex, rowIndex);
        }

        GameObject text = GameObject.Instantiate(numbersPrefab, areaForSpawninNumbers.transform);
        text.GetComponentInChildren<TextMeshProUGUI>().text = $"{rowIndex}";

        return test;
    }

    CharacterClickInteractor SpawnASingleCharacter(CharacterDataAndDefaultState character, int columnIndex, int rowIndex)
    {
        GameObject newUI = GameObject.Instantiate(characterPanelPrefab, areaForSpawningCharacters.transform);
        CharacterClickInteractor newUICharacter = newUI.GetComponent<CharacterClickInteractor>();
        uiToCharacter[newUICharacter] = character.character;
        character.character.myLocation = newUICharacter;

        if (character.onByDefault)
            newUICharacter.RevealPaenl();
        else
            notFoundYet.Add(newUICharacter);

            foreach (string answer in character.character.acceptableAnswers)
            {
                string updatedAnswer = answer.ToLower().Trim();
                if (!possibleAnswersToData.ContainsKey(updatedAnswer))
                {
                    possibleAnswersToData[updatedAnswer] = new List<CharacterData>();
                }

                possibleAnswersToData[updatedAnswer].Add(character.character);
            }

        newUICharacter.OnPanelClicked += () => {
            HighlightClickedCharacter(newUICharacter);
        };

        TextMeshProUGUI[] stuff = newUI.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI text in stuff)
        {
            if (text.name == "Name")
            {
                text.text = character.character.characterName;
            }
            else if (text.name == "Hint")
            {
                text.text = character.character.hintText;
            }
            else
            {
                text.text = $"{(char)(65 + columnIndex)}{rowIndex}";
            }
        }

        return newUICharacter;
    }

    void HighlightClickedCharacter(CharacterClickInteractor newCharacter)
    {
        HighlightHandler.GetHighlightHandler().HighlightJustThisObbject(newCharacter);
        inputField.Select();
        inputField.text = "";
        previousClickedObject = newCharacter;
    }
}

[Serializable]
public class RowOfData
{
    public CharacterDataAndDefaultState[] charactersInThisRow;
}

[Serializable]
public class CharacterDataAndDefaultState
{
    public CharacterData character;
    public  bool onByDefault = false;
}