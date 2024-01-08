using UnityEngine;

namespace Meangpu.Util
{
    [RequireComponent(typeof(Animator))]
    public class AnimationOffsetOnStart : MonoBehaviour
    {
        Animator _animator;
        float _randomTime;

        [SerializeField] Vector2 _minMaxOffset = new(0, 1);
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _randomTime = Random.Range(_minMaxOffset.x, _minMaxOffset.y);
            _animator.Play(0, 0, _randomTime);
        }
    }
}