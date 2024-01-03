using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWanderingState : MonsterBaseState
{
    public MonsterWanderingState(MonsterStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
    }
    public override void Update()
    {
        base.Update();
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
}
