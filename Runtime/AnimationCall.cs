using UnityEngine;

public class AnimationCall : MonoBehaviour
{
    private string _nowState;

    public void changeAnimState(string newState, Animator animator)
    {
        if (_nowState == newState) return;
        animator.Play(newState);
        _nowState = newState;
    }
}
