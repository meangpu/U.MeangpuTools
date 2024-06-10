using UnityEngine;

namespace Meangpu.Brain
{
    [CreateAssetMenu(menuName = "Meangpu/SOChaseBrain")]
    public class SOChaseBrain : Brain
    {
        public string TargetTag = "Player";

        public override void Think(ICanThink _think)
        {
            GameObject target = GameObject.FindGameObjectWithTag(TargetTag);
            if (target)
            {
                // var movement = _think.gameObject.GetComponent<Movement>
            }
        }
    }
}