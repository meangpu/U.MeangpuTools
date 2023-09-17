using UnityEngine;

namespace Meangpu.Util
{
    public class AnimationCall : MonoBehaviour
    {
        protected string _nowState;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _crossFadeTime = .5f;

        public virtual void ChangeAnimationState(string newState)
        {
            if (_nowState == newState) return;
            _animator.Play(newState);
            _animator.CrossFade(newState, _crossFadeTime);
            _nowState = newState;
        }
    }
}