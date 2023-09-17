using UnityEngine;

namespace Meangpu.Util
{
    public class AnimationCall : MonoBehaviour
    {
        protected string _nowState;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _crossFadeTime = .5f;

        /// <summary>
        ///suggest to use AnimationClip as var instead of string to prevent human error
        /// </summary>
        /// <param name="newState"></param>
        public virtual void ChangeAnimationState(string newState)
        {
            if (_nowState == newState) return;
            _animator.Play(newState);
            _animator.CrossFade(newState, _crossFadeTime);
            _nowState = newState;
        }
    }
}