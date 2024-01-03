using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }
    public GameObject Target { get; private set; }
    public MonsterIdleState IdleState { get; }
    public MonsterChasingState ChasingState { get; }
    public MonsterWanderingState WanderingState { get; }
    public Vector2 MonementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    
    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new MonsterIdleState(this);
        ChasingState = new MonsterChasingState(this);
        WanderingState = new MonsterWanderingState(this);

        MovementSpeed = monster.Data.MonsterGroundData.BaseSpeed;
        RotationDamping = monster.Data.MonsterGroundData.BaseRotationDamping;
    }
}
