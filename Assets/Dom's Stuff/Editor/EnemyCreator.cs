using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace Dom.EnemyCreator
{
    public static class EnemyCreator
    {
        [MenuItem("GameObject/Enemy Creator/RangedEnemy", false, 0)]
        public static void CreatorCharacter()
        {
            // Create character with required comonents
            GameObject Capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Capsule.name = "Ranged Enemy";
            Capsule.AddComponent(typeof(Rigidbody));
            Capsule.AddComponent(typeof(NavMeshAgent));
            Capsule.AddComponent(typeof(EnemyBehavior));
            Capsule.AddComponent(typeof(Animator));
            Capsule.GetComponent<EnemyBehavior>().EnemyType = EnemyBehavior.AttackType.RangeHumanoid;
            GameObject Barrel = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Barrel.name = "Fire Point";
            Barrel.transform.parent = Capsule.transform;

            var MR = Capsule.GetComponent<MeshRenderer>();
            MR.material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Dom's Stuff/Materials/Enemy.mat");

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
    }
}
