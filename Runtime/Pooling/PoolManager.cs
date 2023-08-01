using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meangpu.Pool
{
    // note that it is static mode, so there can be error that pool not used when use FAST PLAY MODE 
    public class PoolManager : MonoBehaviour
    {
        public static List<PoolObjectInfo> ObjPools = new();
        static StringBuilder _stringBuilder = new();

        private void Awake()
        {
            ObjPools.Clear();
            _stringBuilder.Clear();
        }

        public static GameObject SpawnObject(GameObject objToSpawn, Vector3 pos, Quaternion rot)
        {
            PoolObjectInfo pool = ObjPools.Find(p => p.Id.Equals(objToSpawn.name));
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
            PoolObjectInfo pool = ObjPools.Find(p => p.Id.Equals(objToSpawn.name));
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
                spawnObj.transform.SetParent(parent);
                spawnObj.transform.SetPositionAndRotation(parent.transform.position, parent.transform.rotation);
                spawnObj.SetActive(true);
            }
            return spawnObj;
        }

        public static void ReturnObjectToPool(GameObject obj)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(obj.name);
            _stringBuilder.Replace("(Clone)", string.Empty);

            PoolObjectInfo pool = ObjPools.Find(p => p.Id.Equals(_stringBuilder.ToString()));
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