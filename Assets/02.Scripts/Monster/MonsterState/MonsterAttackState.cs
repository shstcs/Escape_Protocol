using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    private float _delayAttack;
    public MonsterAttackState(MonsterStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        _delayAttack = 3.1f;
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        _delayAttack -= Time.deltaTime;
        if (stateMachine.Monster.health.IsDead)
            stateMachine.ChangeState(stateMachine.DeadState);
        if(_delayAttack < 0)
        {
            if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.WanderingState);
            }
        }
    }
}
