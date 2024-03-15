using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        player.OnIsPlayerWalking += Player_OnIsPlayerWalking;
        player.OnIsPlayerIdle += Player_OnIsPlayerIdle;
        animator.SetBool("IsWalking",false);
        animator.SetBool("IsIdle",true);
    }
    private void Player_OnIsPlayerWalking(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking",true);
        animator.SetBool("IsIdle",false);
    }
    private void Player_OnIsPlayerIdle(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking",false);
        animator.SetBool("IsIdle",true);
    }
}
