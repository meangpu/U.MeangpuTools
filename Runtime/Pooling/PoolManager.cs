using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Meangpu.Pool
{
    // note that it is static mode, so there can be error that pool not used when use FAST PLAY MODE 
    public class PoolManager : MonoBehaviour
    {
        public static List<PoolObjectInfo> ObjPools = new();

        private GameObject _poolObjectHolder;

        private static GameObject _gameObjectHolder;
        private static GameObject _particleHolder;

        private void Awake() => SetupHolderObject();

        private void SetupHolderObject()
        {
            _poolObjectHolder = new GameObject("==============PoolObjects==============");

            _particleHolder = new GameObject("particlePool");
            _particleHolder.transform.SetParent(_poolObjectHolder.transform);

            _gameObjectHolder = new GameObject("gameobjectPool");
            _gameObjectHolder.transform.SetParent(_poolObjectHolder.transform);
        }

        public static GameObject SpawnObject(GameObject objToSpawn, Vector3 pos, Quaternion rot)
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
                pool.InactiveObj.Remove(spawnObj);
                spawnObj.transform.SetPositionAndRotation(parent.transform.position, parent.transform.rotation);
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
    }
}