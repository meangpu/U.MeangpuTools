using UnityEngine;

namespace Meangpu.Util
{
    public class AnimationCall : MonoBehaviour
    {
        private string _nowState;

        public void ChangeAnimationState(string newState, Animator animator)
        {
            if (_nowState == newState) return;
            animator.Play(newState);
            _nowState = newState;
        }
    }
}