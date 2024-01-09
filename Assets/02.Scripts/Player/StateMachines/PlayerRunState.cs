using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    #region Fields

    private PlayerStamina playerStamina;

    #endregion

    public PlayerRunState(PlayerStateMachine playerStateMachine, PlayerStamina stamina) : base(playerStateMachine)
    {
        playerStamina = stamina;
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();

        stateMachine.Player.GunController.CurrentGun.Anim.SetBool("Run", true);
    }

    public override void Update()
    {
        base.Update();

        // 달리는 동안 스태미나 소모
        RunConsumeStamina();
        //playerStamina.StartStaminaRegeneration();
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.Player.GunController.CurrentGun.Anim.SetBool("Run", false);
    }

    private void RunConsumeStamina()
    {
        // 스태미나 소모량 계산
        float staminaCost = playerStamina.StaminaConsumptionRate * Time.deltaTime;

        // 스태미나 소모
        if (playerStamina.CanConsumeStamina(staminaCost))
        {
            playerStamina.ConsumeStamina(staminaCost);
        }
        else
        {
            // 스태미나가 부족하면 WalkState로 전환
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }
}
