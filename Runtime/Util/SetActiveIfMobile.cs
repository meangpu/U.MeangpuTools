using Meangpu.Datatype;
using UnityEngine;

namespace Meangpu.Util
{
    public class SetActiveIfMobile : MonoBehaviour
    {
        [SerializeField] GameObjectWithBool[] objectList;

        private void Start()
        {
            if (
Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
            {
                foreach (GameObjectWithBool obj in objectList)
                {
                    obj.SetActiveByState();
                }
            }
        }
    }
}