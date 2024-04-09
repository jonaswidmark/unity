using UnityEngine;

public class AnimationCompleteListener : StateMachineBehaviour
{
    // Händelse som utlöses när en animation är klar
    public event System.Action OnAnimationComplete;

    // Utlöser händelsen när state exit inträffar
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("AnimationCompleteListener");
        OnAnimationComplete?.Invoke();
    }
}
