using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyState : EnemyState
{
    private Rigidbody m_Body;
    private RangedEnemyStateMachine m_RangedStateMachine;

    public ShootingEnemyState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        m_Body = stateMachine.GetComponent<Rigidbody>();
        m_RangedStateMachine = m_StateMachine as RangedEnemyStateMachine;
    }

    public override void ExecuteUpdate()
    {
        Vector3 directiontoplayer = m_StateMachine.m_Player.transform.position - m_StateMachine.transform.position;
        
        if ((m_StateMachine.transform.position - m_StateMachine.m_Player.GetPlayerPosition).magnitude < 5)
        {
            m_Body.velocity = directiontoplayer.normalized * -1;
        }
        else if ((m_StateMachine.transform.position - m_StateMachine.m_Player.GetPlayerPosition).magnitude > 7.5f)
        {
            m_Body.velocity = directiontoplayer.normalized;
        }
        else
        {
            m_Body.velocity = Vector3.zero;
        }

        if (!m_RangedStateMachine.oncooldown) //also check for obstruction
        {
            m_RangedStateMachine.InstanciateBullet(); 
        }

        if ((m_StateMachine.transform.position - m_StateMachine.m_Player.GetPlayerPosition).magnitude > 15 && m_StateMachine.m_CanChangeState)
        {
            m_StateMachine.ChangeState(new IdleEnemyState(m_StateMachine));
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
