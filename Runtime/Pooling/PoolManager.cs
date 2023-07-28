using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Meangpu.Pool
{
    public class PoolManager : MonoBehaviour
    {
        public static List<PoolObjectInfo> ObjPools = new();

        private GameObject _poolObjectHolder;

        private static GameObject _gameObjectHolder;
        private static GameObject _particleHolder;

        public static PoolType PoolType;

        private void Awake() => SetupHolderObject();

        private void SetupHolderObject()
        {
            _poolObjectHolder = new GameObject("==============PoolObjects==============");

            _particleHolder = new GameObject("particlePool");
            _particleHolder.transform.SetParent(_poolObjectHolder.transform);

            _gameObjectHolder = new GameObject("gameobjectPool");
            _gameObjectHolder.transform.SetParent(_poolObjectHolder.transform);
        }

        public static GameObject SpawnObject(GameObject objToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GAMEOBJECT)
        {
            PoolObjectInfo pool = ObjPools.Find(p => p.Id == objToSpawn.name);
            if (pool == null)
            {
                pool = new PoolObjectInfo() { Id = objToSpawn.name };
                ObjPools.Add(pool);
            }

            GameObject spawnObj = pool.InactiveObj.FirstOrDefault();

            if (spawnObj == null)
            {
                spawnObj = Instantiate(objToSpawn, pos, rot);

                GameObject parentObj = SetParentObj(poolType);
                if (parentObj != null) spawnObj.transform.SetParent(parentObj.transform);
            }
            else
            {
                spawnObj.transform.position = pos;
                spawnObj.transform.rotation = rot;
                pool.InactiveObj.Remove(spawnObj);
                spawnObj.SetActive(true);
            }
            return spawnObj;
        }

        public static GameObject SpawnObject(GameObject objToSpawn, Transform parent)
        {
            PoolObjectInfo pool = ObjPools.Find(p => p.Id == objToSpawn.name);
            if (pool == null)
            {
                pool = new PoolObjectInfo() { Id = objToSpawn.name };
                ObjPools.Add(pool);
            }

            GameObject spawnObj = pool.InactiveObj.FirstOrDefault();

            if (spawnObj == null)
            {
                spawnObj = Instantiate(objToSpawn, parent);
            }
            else
            {
                spawnObj.transform.SetParent(parent);
                pool.InactiveObj.Remove(spawnObj);
                spawnObj.SetActive(true);
            }
            return spawnObj;
        }

        public static void ReturnObjectToPool(GameObject obj)
        {
            string checkName = obj.name.Replace("(Clone)", string.Empty);
            PoolObjectInfo pool = ObjPools.Find(p => p.Id == checkName);
            if (pool == null)
            {
                Debug.LogWarning("try to return object that not in pool yet");
            }
            else
            {
                obj.SetActive(false);
                pool.InactiveObj.Add(obj);
            }
        }

        private static GameObject SetParentObj(PoolType type)
        {
            return type switch
            {
                PoolType.PARTICLE => _particleHolder,
                PoolType.GAMEOBJECT => _gameObjectHolder,
                PoolType.NONE => null,
                _ => null,
            };
        }
    }
}