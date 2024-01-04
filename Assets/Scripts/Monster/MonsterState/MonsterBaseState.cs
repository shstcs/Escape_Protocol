using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;
    private List<Collider> hitTargetList = new List<Collider>();
    private LayerMask targetMask;
    private RaycastHit _hit;

    #region IState
    public MonsterBaseState(MonsterStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }
    public virtual void Enter()
    {
        stateMachine.Monster.Agent.speed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
    }
    public virtual void Exit()
    {
    }
    public virtual void Update()
    {
        View();
        if (!stateMachine.Monster.health.IsDead)
            Move();
        else
            stateMachine.ChangeState(stateMachine.DeadState);
        if(stateMachine.Target.IsDead)
            stateMachine.Monster.enabled = false;

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
        stateMachine.Monster.Agent.SetDestination(stateMachine.Target.transform.position);
        Rotate(stateMachine.Target.transform.position - stateMachine.Monster.transform.position);
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
    protected bool IsInChaseRange()
    {
        if (stateMachine.Target.IsDead)
        {
            stateMachine.Monster.enabled = false;
            return false;
        }
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Monster.Data.PlayerChasingRange * stateMachine.Monster.Data.PlayerChasingRange;
    }
    protected bool IsInAttackRange()
    {
        if (stateMachine.Target.IsDead)
        {
            stateMachine.Monster.enabled = false;
            return false;
        }
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Monster.Data.AttackRange * stateMachine.Monster.Data.AttackRange;
    }
    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += stateMachine.Monster.transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }
    private void View()
    {
        targetMask = 8;
        float lookingAngle = stateMachine.Monster.transform.eulerAngles.x;
        Vector3 rightDir = BoundaryAngle(stateMachine.Monster.Data.ViewAngle * 0.5f);
        Vector3 leftDir = BoundaryAngle(- stateMachine.Monster.Data.ViewAngle * 0.5f);
        Vector3 lookDir = BoundaryAngle(lookingAngle);

        Debug.DrawRay(stateMachine.Monster.transform.position, rightDir * 10f, Color.blue);
        Debug.DrawRay(stateMachine.Monster.transform.position, leftDir * 10f, Color.blue);
        Debug.DrawRay(stateMachine.Monster.transform.position, lookDir * 10f, Color.cyan);

        hitTargetList.Clear();

        Collider[] targets = Physics.OverlapSphere(stateMachine.Monster.transform.position, 
            100f, targetMask);
        if (targets.Length == 0)
        {
            Debug.Log("0단계"); 
            return;
        }
        foreach(Collider target in targets)
        {
            Debug.Log("1단계");
            Vector3 targetPos = target.transform.position;
            Vector3 targetDir = (targetPos - stateMachine.Monster.transform.position).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(target.transform.position - stateMachine.Monster.transform.position,
                targetDir))*Mathf.Deg2Rad;
            if(targetAngle <= stateMachine.Monster.Data.ViewAngle * 0.5f && Physics.Raycast(stateMachine.Monster.transform.position,
                targetDir, out _hit, stateMachine.Monster.Data.PlayerChasingRange))
            {
                Debug.Log(_hit.transform.name);
                hitTargetList.Add(target);
                Debug.DrawRay(stateMachine.Monster.transform.position, targetPos, Color.red);
            }
        }
    }
    #endregion
}
