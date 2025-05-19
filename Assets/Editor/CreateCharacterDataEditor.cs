
using UnityEditor;
using UnityEngine;

public class CreateCharacterDataEditor : EditorWindow
{
    private string assetName = "NewCharacterData";

    [MenuItem("Tools/Character Data Creator")]
    public static void ShowWindow()
    {
        GetWindow<CreateCharacterDataEditor>("Character Data Creator");
    }

    private GUIStyle textAreaStyle;

    private void OnEnable()
    {
        textAreaStyle = new GUIStyle(EditorStyles.textArea);
        textAreaStyle.wordWrap = true;
        textAreaStyle.normal.textColor = Color.white;
    }

    private void OnGUI()
    {
        GUILayout.Label("Create CharacterData", EditorStyles.boldLabel);

        assetName = EditorGUILayout.TextArea(assetName, textAreaStyle, GUILayout.Height(160));

        if (GUILayout.Button("Create CharacterData Asset"))
        {
            CreateCharacterDataAsset(assetName);
        }
    }

    private void CreateCharacterDataAsset(string names)
    {
        string[] splitNames = names.Split(',');

        foreach (string name in splitNames)
        {
            string nameTrimmed = name.Trim();

            if (string.IsNullOrEmpty(nameTrimmed))
                continue;

            Debug.Log(nameTrimmed);

            CharacterData asset = ScriptableObject.CreateInstance<CharacterData>();
            asset.characterName = nameTrimmed;

            string path = $"Assets/Characters/{nameTrimmed}.asset";
            path = AssetDatabase.GenerateUniqueAssetPath(path);

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
        }

        /*EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;*/
    }
}
