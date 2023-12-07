using UnityEngine;

namespace Meangpu.Util
{
    public class SetActiveBlink : BlinkSystem
    {
        [SerializeField] GameObject _target;
        public override void BlinkAction(bool nowState) => _target.SetActive(nowState);
    }
}
