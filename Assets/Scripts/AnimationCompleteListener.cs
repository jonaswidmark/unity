using UnityEngine;

public class AnimationCompleteListener : StateMachineBehaviour
{
    public event System.Action OnAnimationComplete;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnAnimationComplete?.Invoke();
    }
}
