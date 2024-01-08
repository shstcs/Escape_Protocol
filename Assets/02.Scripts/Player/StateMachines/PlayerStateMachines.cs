using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private float _jumpStartTime; // 점프 시작 시간

    public Player Player { get; }

    // States
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }
    public PlayerAttackState AttackState { get; }

    // 
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }

    public float JumpStartTime
    {
        get { return _jumpStartTime; }
        private set { _jumpStartTime = value; }
    }

    public Transform MainCameraTransform { get; set; }

    public PlayerStamina Stamina { get; private set; }

    public PlayerStateMachine(Player player, PlayerStamina stamina)
    {
        this.Player = player;
        // PlayerStamina 초기화
        Stamina = stamina;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this, stamina);
        RunState = new PlayerRunState(this, stamina);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        AttackState = new PlayerAttackState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundedData.BaseSpeed;
        RotationDamping = player.Data.GroundedData.BaseRotationDamping;
    }

    public void SetJumpStartTime(float time)
    {
        _jumpStartTime = time;
    }
}
