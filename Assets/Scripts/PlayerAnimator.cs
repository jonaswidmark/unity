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
        /* inputManager.OnIsMovingForward += InputManager_OnIsMovingForward;
        inputManager.OnIsMovingBackwards += InputManager_OnIsMovingBackwards;
        inputManager.OnIsMovingLeft += InputManager_OnIsMovingLeft;
        inputManager.OnIsMovingRight += InputManager_OnIsMovingRight;
        inputManager.OnIsJumping += InputManager_OnIsJumping;
        inputManager.OnNotMoving += InputManager_OnNotMoving; */
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

    /* private void InputManager_OnIsMovingForward(object sender, System.EventArgs e)
    {
        animator.SetBool("IsMovingForward",true);
        //animator.SetBool("WalkLeft",false);
        //animator.SetBool("WalkRight",false);
    }
    private void InputManager_OnIsMovingBackwards(object sender, System.EventArgs e)
    {
        animator.SetBool("IsMovingBackwards",true);
        animator.SetBool("WalkLeft",false);
        animator.SetBool("IsMovingForward",false);
        animator.SetBool("IsMovingForward",false);
    }
    private void InputManager_OnNotMoving(object sender, System.EventArgs e)
    {
        animator.SetBool("IsMovingBackwards",false);
        animator.SetBool("IsMovingForward",false);
        animator.SetBool("WalkLeft",false);
        animator.SetBool("WalkRight",false);
    }
    private void InputManager_OnIsMovingLeft(object sender, System.EventArgs e)
    {
        animator.SetBool("IsMovingBackwards",false);
        animator.SetBool("WalkLeft",true);
        animator.SetBool("IsMovingForward",false);
        animator.SetBool("WalkRight",false);
    }
    private void InputManager_OnIsMovingRight(object sender, System.EventArgs e)
    {
        animator.SetBool("WalkRight",true);
        animator.SetBool("WalkLeft",false);
        animator.SetBool("IsMovingForward",false);
        animator.SetBool("IsMovingBackwards",false);
    } */
}
