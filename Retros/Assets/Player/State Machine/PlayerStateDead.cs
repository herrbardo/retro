using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : PlayerStateBase
{
    public PlayerStateDead(PlayerStateManager context)
    {
        Context = context;
    }

    public override void StartState()
    {
        
    }

    public override void EnterState()
    {
        Context.Animator.SetBool("isDead", true);
        Context.PlayerRigidbody.isKinematic = true;
        Context.AuraPlayer.Stop();
    }

    public override void ExitState()
    {
        
    }
}
