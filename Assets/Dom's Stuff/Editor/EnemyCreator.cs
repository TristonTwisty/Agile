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
            GameObject Shooter = new GameObject("Shooter Enemy", typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent), typeof(EnemyBehavior), typeof(ShootingAI));
            // Focus the newly created character
            Selection.activeGameObject = Shooter;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem("GameObject/Enemy Creator/Melee Enemy", false, 0)]
        public static void CreateMeleeEnemy()
        {
            GameObject Melee = new GameObject("Melee Enemy", typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent), typeof(EnemyBehavior), typeof(MeleeAI));
            // Focus on created character
            Selection.activeGameObject = Melee;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem("GameObject/Enemy Creator/Drone Enemy", false, 0)]
        public static void CreateDroneEnemy()
        {
            GameObject Drone = new GameObject("Drone Enemy", typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent), typeof(EnemyBehavior), typeof(DroneAI));
            // Focus on created character
            Selection.activeGameObject = Drone;
            SceneView.FrameLastActiveSceneView();
        }
    }
}
