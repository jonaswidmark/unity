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
        player.OnIsPlayerWalking += Player_OnIsPlayerWalking;
        player.OnIsPlayerWalking += Player_OnIsPlayerIdle;
        //animator.SetBool("IsWalking",false);
        /* 
        inputManager = InputManager.Instance;
        animator.SetBool("IsMovingForward",false);
        animator.SetBool("IsMovingBackwards",false);
        animator.SetBool("WalkLeft",false);
        animator.SetBool("WalkRight",false); */
    }
    private void Player_OnIsPlayerWalking(object sender, EventArgs e)
    {
        Debug.Log(animator);
        animator.SetBool("IsWalking",true);
    }
    private void Player_OnIsPlayerIdle(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking",false);
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
