using UnityEngine;

namespace Meangpu.Util
{
    public class SetMaterialBlink : BlinkSystem
    {
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _onMat;
        [SerializeField] Material _offMat;

        public override void BlinkAction(bool nowState)
        {
            if (nowState) _renderer.material = _onMat;
            else _renderer.material = _offMat;
        }
    }
}
