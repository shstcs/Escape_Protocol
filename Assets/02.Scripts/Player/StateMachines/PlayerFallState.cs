using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private float _timeOnGround; // 바닥에 닿은 시간 저장
    private float _fallEndTime; // 낙하 종료 시간

    private PlayerHealth _health;

    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        _health = GameObject.Find("Player").GetComponent<PlayerHealth>();

        // 바닥에 닿은 시간 초기화
        _timeOnGround = 0f;

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        _timeOnGround += Time.deltaTime;

        if (stateMachine.Player.Controller.isGrounded)
        {
            // 바닥에 닿았을 때 데미지 체크 및 낙하 종료 시간 초기화
            int fallDamage = CalculateFallDamage();
            if (fallDamage > 0)
            {
                _health.TakeDamage(fallDamage);
            }
            // 낙하 종료 시간 초기화
            _fallEndTime = Time.time;

            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

    // 낙하 시간 계산
    public float CalculateFallTime()
    {
        return _fallEndTime - stateMachine.JumpStartTime;
    }

    private int CalculateFallDamage()
    {
        //Debug.Log(_timeOnGround + "초");

        // 일정 시간 이상일 때 데미지 적용
        if (_timeOnGround >= 1.0f)
        {
            // 시간에 따라 데미지 계산
            return Mathf.RoundToInt(Mathf.Abs(_timeOnGround * 10f));
        }
        return 0;
    }
}
