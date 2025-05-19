using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ContextMenu
{

    [MenuItem("Assets/Run Custom Command", true)]
    private static bool ValidateRunCustomCommand()
    {
        foreach (var data in Selection.objects)
        {
            if (data is CharacterData)
            {
                return true;
            }
        }
        return false;
    }

    [MenuItem("Assets/Run Custom Command")]
    private static void RunCustomCommand()
    {
        foreach (var data in Selection.objects)
        {
            if (data is CharacterData myData)
            {
                myData.SetAcceptableAnswers();
                EditorUtility.SetDirty(myData);
            }
        }
    }
}
