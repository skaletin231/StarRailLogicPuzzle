using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This grid manager needs to track click interactors since that is what tracks revealed status
public class GridManager
{
    private static GridManager Instance;

    private CharacterClickInteractor[,] CharacterClickInteractorGrid = null;

    private GridManager() { }

    public static GridManager GetGridManager()
    {
        return Instance ??= new GridManager();
    }
    
    public void CreateGrid(int x, int y)
    {
        CharacterClickInteractorGrid = new CharacterClickInteractor[x,y];
    }

    public void SetEntireRow(int row, CharacterClickInteractor[] characters)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            SetPosition(i, row, characters[i]);
        }
    }

    public void SetPosition(int x, int y, CharacterClickInteractor character)
    {
        CharacterClickInteractorGrid[x, y] = character;
    }

    public CharacterClickInteractor GetCharacterAtPosition(int x, int y)
    {
        return CharacterClickInteractorGrid[x,y];
    }
}
