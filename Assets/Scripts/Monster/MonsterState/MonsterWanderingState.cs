using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterWanderingState : MonsterBaseState
{
    public MonsterWanderingState(MonsterStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1f;
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
        else
        {
            stateMachine.Monster.Agent.SetDestination(GetWanderLocation());
        }
    }
    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(stateMachine.Monster.transform.position + (Random.onUnitSphere) * Random.Range(5f, 20f), out hit, 20f, NavMesh.AllAreas);
        int i = 0;
        while (Vector3.Distance(stateMachine.Monster.transform.position, hit.position) < stateMachine.Monster.Data.PlayerChasingRange)
        {
            NavMesh.SamplePosition(stateMachine.Monster.transform.position + (Random.onUnitSphere) * Random.Range(5f, 20f), out hit, 20f, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }
}
