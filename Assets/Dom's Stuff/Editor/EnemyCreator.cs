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
            GameObject Face = new GameObject("Face");
            Face.transform.parent = Shooter.transform;
            GameObject FirePoint = new GameObject("Firepoint");
            FirePoint.transform.parent = Shooter.transform;

            Shooter.GetComponent<ShootingAI>().Face = Face.transform;
            Shooter.GetComponent<ShootingAI>().FirePoint = FirePoint.transform;
            // Focus the newly created character
            /* Selection.activeGameObject = Shooter;
            SceneView.FrameLastActiveSceneView(); */
        }

        [MenuItem("GameObject/Enemy Creator/Melee Enemy", false, 0)]
        public static void CreateMeleeEnemy()
        {
            GameObject Melee = new GameObject("Melee Enemy", typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent), typeof(EnemyBehavior), typeof(MeleeAI));
            GameObject Face = new GameObject("Face");
            Face.transform.parent = Melee.transform;

            Melee.GetComponent<MeleeAI>().Face = Face.transform;
            // Focus on created character
            /* Selection.activeGameObject = Shooter;
            SceneView.FrameLastActiveSceneView(); */
        }

        [MenuItem("GameObject/Enemy Creator/Drone Enemy", false, 0)]
        public static void CreateDroneEnemy()
        {
            GameObject Drone = new GameObject("Drone Enemy", typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent), typeof(EnemyBehavior), typeof(DroneAI));
            GameObject FirePoint = new GameObject("Firepoint");
            FirePoint.transform.parent = Drone.transform;

            Drone.GetComponent<DroneAI>().FirePoint = FirePoint.transform;
            // Focus on created character
            /* Selection.activeGameObject = Shooter;
            SceneView.FrameLastActiveSceneView(); */
        }
    }
}
