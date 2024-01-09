using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        stateMachine.Player.ForceReceiver.Jump(stateMachine.JumpForce);
        base.Enter();

        // 점프 시작 시간을 PlayerStateMachine에 기록
        stateMachine.SetJumpStartTime(Time.time);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (stateMachine.Player.Controller.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
}
