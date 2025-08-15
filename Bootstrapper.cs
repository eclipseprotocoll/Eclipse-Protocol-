using UnityEditor;
using UnityEngine;

namespace EclipseProtocol
{
    public class Bootstrapper
    {
        [MenuItem("Eclipse Protocol/Setup/Starter Scene")]
        public static void CreateStarterScene()
        {
            if (EditorSceneManager.GetActiveScene().isDirty)
            {
                if (!EditorUtility.DisplayDialog("Unsaved Changes", "Current scene has unsaved changes. Continue?", "Yes", "No"))
                    return;
            }

            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(newScene, "Assets/Scenes/StarterScene.unity");
            Debug.Log("Starter scene created. Add Player prefab and spawn points.");
        }
    }
}
