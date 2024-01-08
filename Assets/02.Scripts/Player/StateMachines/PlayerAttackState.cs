using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedForce;

    AttackInfoData attackInfoData;
    PlayerAttackData playerAttackData;

    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0.3f; // PlayerSO의 값과 동일하도록
        // runspeedmoodifier 추가
        stateMachine.JumpForce = 6f;
        base.Enter();
        alreadyAppliedForce = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }

    //public override void Update()
    //{
    //    base.Update();

    //    ForceMove();

    //    float normalizedTime = 0.5f;
    //    if (normalizedTime < 1f)
    //    {
    //        if (normalizedTime >= attackInfoData.ForceTransitionTime)
    //            TryApplyForce();
    //    }
    //    else
    //    {
    //        stateMachine.ChangeState(stateMachine.IdleState);
    //    }
    //}

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}