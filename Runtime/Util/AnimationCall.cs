using UnityEngine;

namespace Meangpu.Util
{
    public class AnimationCall : MonoBehaviour
    {
        protected string _nowState;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _crossFadeTime = .5f;
        [SerializeField] protected bool _useCrossFade = true;
        [SerializeField] protected bool _canRecallCurrentAnimation;

        /// <summary>
        ///suggest to use AnimationClip as var instead of string to prevent human error
        /// </summary>
        /// <param name="newState"></param>
        public virtual void ChangeAnimationState(string newState)
        {
            if (string.IsNullOrEmpty(newState))
            {
                Debug.Log("newState is null, cannot change state", gameObject);
                return;
            }

            if (!_canRecallCurrentAnimation && _nowState == newState) return;
            if (_useCrossFade) _animator.CrossFadeInFixedTime(newState, _crossFadeTime);
            else _animator.Play(newState);
            _nowState = newState;
        }

        public virtual void ChangeAnimationState(AnimationClip newState)
        {
            if (newState == null)
            {
                Debug.Log("AnimationClip is null, cannot change state", gameObject);
                return;
            }

            if (!_canRecallCurrentAnimation && _nowState == newState.name) return;
            if (_useCrossFade) _animator.CrossFadeInFixedTime(newState.name, _crossFadeTime);
            else _animator.Play(newState.name);
            _nowState = newState.name;
        }
    }
}