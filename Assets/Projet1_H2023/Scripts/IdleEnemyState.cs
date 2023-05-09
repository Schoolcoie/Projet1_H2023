using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : EnemyState
{
    private Rigidbody m_Body;
    private Coroutine m_IdleCoroutine;

    public IdleEnemyState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        m_Body = stateMachine.GetComponent<Rigidbody>();
        m_IdleCoroutine = m_StateMachine.StartCoroutine(IdleRoutine());
    }

    public override void ExecuteUpdate()
    {
        //after 2 seconds go into roaming
        //if player spotted go into shooting if m_StateMachine is RangedEnemyStateMachine
        //if player spotted go into chasing if m_StateMachine is not RangedEnemyStateMachine
    }

    public override void ExecuteFixedUpdate()
    {
    }

    public override void ExecuteOnCollisionEnter(Collision collision)
    {
    }
    public override void ExecuteOnCollisionExit(Collision collision)
    {
    }

    public override void ExecuteOnTriggerEnter(Collider other)
    {
    }

    public override void ExecuteOnTriggerExit(Collider other)
    {
    }

    private IEnumerator IdleRoutine()
    {
        yield return new WaitForSeconds(2);
        m_StateMachine.ChangeState(new RoamingEnemyState(m_StateMachine));
    }
}
