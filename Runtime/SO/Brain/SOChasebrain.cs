using UnityEngine;

namespace Meangpu.Brain
{
    [CreateAssetMenu(menuName = "Brain/SOChasebrain")]
    public class SOChasebrain : Brain
    {
        public string TargetTag = "Player";

        public override void Think(Thinker _think)
        {
            GameObject target = GameObject.FindGameObjectWithTag(TargetTag);
            if (target)
            {
                // var movement = _think.gameObject.GetComponent<Movement>
            }
        }
    }
}