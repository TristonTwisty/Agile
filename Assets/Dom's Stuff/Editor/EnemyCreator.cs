using UnityEditor;
using UnityEngine;

public class EnemyCreator : EditorWindow
{
    EnemyObject Enemy;

    [MenuItem("Window/ Enemy Creator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EnemyCreator>("Enemy Creator");
    }

    private void OnGUI()
    {

    }
    static void CreatePrefab()
    {

    }
}
