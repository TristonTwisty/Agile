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
            // Create character with required comonents
            GameObject Capsule = new GameObject("Ranged Enemy");
            Capsule.AddComponent(typeof(Rigidbody));
            Capsule.AddComponent(typeof(NavMeshAgent));
            Capsule.AddComponent(typeof(EnemyBehavior));
            Capsule.GetComponent<EnemyBehavior>().EnemyType = EnemyBehavior.AttackType.RangeHumanoid;
            GameObject FirePoint = new GameObject("Fire Point");
            FirePoint.transform.parent = Capsule.transform;

            // Initialize rigibody
            var RB = Capsule.GetComponent<Rigidbody>();

            RB.angularDrag = 0f;
            RB.useGravity = true;
            RB.isKinematic = false;
            RB.interpolation = RigidbodyInterpolation.Interpolate;
            RB.freezeRotation = false;

            // Focus the newly created character

            Selection.activeGameObject = Capsule;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem("GameObject/Enemy Creator/Melee Enemy", false, 0)]
        public static void CreateMeleeEnemy()
        {
            GameObject Enemy = new GameObject("Melee Enemy");
            Enemy.AddComponent(typeof(Rigidbody));
            Enemy.AddComponent(typeof(NavMeshAgent));
            Enemy.AddComponent(typeof(EnemyBehavior));
            Enemy.GetComponent<EnemyBehavior>().EnemyType = EnemyBehavior.AttackType.MeleeHumanoid;

            // Inbitalize rigibody
            var RB = Enemy.GetComponent<Rigidbody>();

            RB.angularDrag = 0f;
            RB.useGravity = true;
            RB.isKinematic = false;
            RB.interpolation = RigidbodyInterpolation.Interpolate;
            RB.freezeRotation = false;

            // Focus on created character
            Selection.activeGameObject = Enemy;
            SceneView.FrameLastActiveSceneView();
        }
    }
}
