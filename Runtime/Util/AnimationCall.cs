using UnityEngine;

namespace Meangpu.Util
{
    public class AnimationCall : MonoBehaviour
    {
        protected string _nowState;

        public virtual void ChangeAnimationState(string newState, Animator animator)
        {
            if (_nowState == newState) return;
            animator.Play(newState);
            _nowState = newState;
        }
    }
}