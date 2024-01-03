using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;
    protected readonly MonsterGroundData monsterGroundData;

    #region IState
    public MonsterBaseState(MonsterStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        monsterGroundData = stateMachine.Monster.Data.MonsterGroundData;
    }
    public virtual void Enter()
    {

    }
    public virtual void Exit()
    {

    }
    public virtual void Update()
    {
        Move();
    }
    public virtual void HandleInput()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    #endregion
    #region Methods
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
    }
    private void Move()
    {
        Vector3 movementDirection = GetMovementDireciton();
        Rotate(movementDirection);
        stateMachine.Monster.Agent.SetDestination(stateMachine.Target.transform.position);
    }
    private Vector3 GetMovementDireciton()
    {
        return (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).normalized;
    }
    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            stateMachine.Monster.transform.rotation = Quaternion.Slerp(stateMachine.Monster.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }
    protected float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }
    protected bool IsInChaseRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Monster.Data.PlayerChasingRange * stateMachine.Monster.Data.PlayerChasingRange;
    }
    #endregion
}
