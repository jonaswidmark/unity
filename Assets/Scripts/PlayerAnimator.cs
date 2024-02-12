using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private InputManager inputManager;
    private Player player;
    private bool isPlayerGrounded;
    private void Start()
    {
        player = GetComponent<Player>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        inputManager = InputManager.Instance;
        animator.SetBool("IsMovingForward",false);
        animator.SetBool("IsMovingBackwards",false);
        animator.SetBool("WalkLeft",false);
        animator.SetBool("WalkRight",false);
    }

    /* private void InputManager_OnIsJumping(object sender, EventArgs e)
    {
        bool isPlayerGrounded = player.GetIsGrounded();
        bool isPlayerInMovement = player.GetIsInMovement();
        bool isPlayerMovingForward = player.GetIsMovingForward();
        if(isPlayerGrounded && !isPlayerInMovement)
        {
            animator.SetTrigger("Jump");
        }
        else if(isPlayerMovingForward && isPlayerGrounded)
        {
            animator.SetTrigger("JumpRun");
        }
    } */

    
}
