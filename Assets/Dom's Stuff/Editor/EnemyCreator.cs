using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace Dom.EnemyCreator
{
    public static class EnemyCreator
    {
        [MenuItem("GameObject/Enemy Creator/Ranged Enemy", false, 0)]
        public static void CreateRangedEnemy()
        {
            // Focus the newly created character
            //Selection.activeGameObject = Shooter;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem("GameObject/Enemy Creator/Melee Enemy", false, 0)]
        public static void CreateMeleeEnemy()
        {
            // Focus on created character
            //Selection.activeGameObject = Melee;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem("GameObject/Enemy Creator/Drone Enemy", false, 0)]
        public static void CreateDroneEnemy()
        {
            // Focus on created character
            //Selection.activeGameObject = Drone;
            SceneView.FrameLastActiveSceneView();
        }
    }
}
