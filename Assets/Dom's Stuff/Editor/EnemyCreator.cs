using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using HutongGames.PlayMaker.Actions;

namespace Dom.EnemyCreator
{
    public static class EnemyCreator
    {
        [MenuItem("GameObject/Enemy Creator/Enemy", false, 0)]
        public static void CreatorCharacter()
        {
            // Create character with required comonents
            GameObject Capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Capsule.name = "Enemy";
            Capsule.AddComponent(typeof(Rigidbody));
            Capsule.AddComponent(typeof(NavMeshAgent));
            Capsule.AddComponent(typeof(EnemyLogic));
            Capsule.AddComponent(typeof(EnemyMovement));
            Capsule.AddComponent(typeof(EnemyAttack));

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
