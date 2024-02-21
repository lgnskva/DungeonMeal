using UnityEditor.SceneManagement;
using UnityEditor;

[InitializeOnLoad]
public class DefaultScene
{
    static DefaultScene()
    {
        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
    }
}