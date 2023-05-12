using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemyState : EnemyState
{
    private Rigidbody m_Body;
    private float m_Speed = 200.0f;
    private Coroutine m_RoamingCoroutine;

    public RoamingEnemyState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        m_Body = stateMachine.GetComponent<Rigidbody>();
    }

    public override void ExecuteUpdate()
    {

        if ((m_StateMachine.transform.position - m_StateMachine.m_Player.GetPlayerPosition).magnitude < 10)
        {
            if (m_StateMachine is RangedEnemyStateMachine)
            {
                m_StateMachine.ChangeState(new ShootingEnemyState(m_StateMachine));
            }
            else
            {
                m_StateMachine.ChangeState(new ChasingEnemyState(m_StateMachine));
            }
        }
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
}
