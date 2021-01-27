using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=r2uY_JYvFZk&feature=youtu.be

public static class ObjectPooling
{
    // prefab object and its pool
    private static Dictionary<GameObject, Pool> Pools;

    // Checks if pool is empty
    // activates object at position and rotation
    public static GameObject Spawn(GameObject Prefab, Vector3 Position, Quaternion Rotation)
    {
        if(Pools == null)
        {
            Pools = new Dictionary<GameObject, Pool>();
        }

        if(Pools != null && Pools.ContainsKey(Prefab) == false)
        {
            Pools[Prefab] = new Pool(Prefab);
        }

        return Pools[Prefab].Spawn(Position, Rotation);
    }

    // Checks if pool is member
    // if is, deactivate object
    // else, destroy object
    public static void DeSpawn(GameObject OBJ)
    {
        PoolMember poolmember = OBJ.GetComponent<PoolMember>();

        if(poolmember == null)
        {
            GameObject.Destroy(OBJ);
        }
        else
        {
            poolmember.MyPool.Despawn(OBJ);
        }
    }

    // Stores the prefab type
    // Stores a stack of X prefab
    private class Pool
    {
        private int CurrentIndex;
        private Stack<GameObject> InactiveObjects;
        private GameObject Prefab;

        public Pool(GameObject _prefab)
        {
            Prefab = _prefab;
            InactiveObjects = new Stack<GameObject>();
        }

        public GameObject Spawn(Vector3 Position, Quaternion Rotation)
        {
            GameObject OBJ;

            if(InactiveObjects.Count == 0)
            {
                OBJ = (GameObject)GameObject.Instantiate(Prefab, Position, Rotation);
                OBJ.name = Prefab.name + "_" + CurrentIndex;
                CurrentIndex++;
                OBJ.AddComponent<PoolMember>().MyPool = this;
            }
            else
            {
                OBJ = InactiveObjects.Pop();

                if(OBJ == null)
                {
                    return Spawn(Position, Rotation);
                }
            }

            OBJ.transform.position = Position;
            OBJ.transform.rotation = Rotation;
            OBJ.SetActive(true);
            return OBJ;
        }

        public void Despawn(GameObject OBJ)
        {
            OBJ.SetActive(false);
            InactiveObjects.Push(OBJ);
        }
    }

    private class PoolMember : MonoBehaviour
    {
        private Pool _MyPool;

        public Pool MyPool { get { return _MyPool; } set { _MyPool = value; } }
    }
}
