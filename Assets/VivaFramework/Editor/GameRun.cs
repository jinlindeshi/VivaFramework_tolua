
using UnityEditor;
using UnityEditor.SceneManagement;

public class GameRun
{
    private static string ScenePath = "Assets/Enter.unity";

   
    [MenuItem("Run/Run Game")]
    static void Run()
    {
        EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}
