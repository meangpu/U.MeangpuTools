using System.Collections.Generic;
using VInspector;
using UnityEngine;

namespace Meangpu.Util
{
    public abstract class SpawnListObject<T> : MonoBehaviour where T : Object
    {
        [SerializeField] List<T> _objectToSpawn;
        [SerializeField] Transform _parentTrans;

        [Button]
        protected virtual void EditorSpawnEmptyObj()
        {
            if (_parentTrans == null) _parentTrans = gameObject.transform;
            KillAllChild.KillAllChildInTransform(_parentTrans);
            foreach (T obj in _objectToSpawn)
            {
                GameObject nowObj = new(obj.name);
                nowObj.transform.SetParent(_parentTrans);
            }
        }
    }
}