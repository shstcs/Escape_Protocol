using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundedState
{
    private PlayerStamina playerStamina;

    public PlayerWalkState(PlayerStateMachine playerStateMachine, PlayerStamina stamina) : base(playerStateMachine)
    {
        playerStamina = stamina;
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        // 달리기 상태로 전환하기 위한 충분한 스태미나가 있는지 확인
        if (playerStamina.CanConsumeStamina(playerStamina.StaminaConsumptionRate))
        {
            base.OnRunStarted(context);
            stateMachine.ChangeState(stateMachine.RunState);
        }
    }
}
