using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class TransformPosRotResetter : MonoBehaviour
    {
        [SerializeField] TransformPosRotResetterObject[] _objectToReset;

        [Button]
        public void DoResetAllPosRotWorld()
        {
            foreach (TransformPosRotResetterObject obj in _objectToReset) obj.DoResetWorldPosRot();
        }

        [Button]
        public void DoResetAllPosRotLocal()
        {
            foreach (TransformPosRotResetterObject obj in _objectToReset) obj.DoResetLocalPosRot();
        }
    }
}
