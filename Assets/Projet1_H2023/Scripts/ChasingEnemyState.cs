using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyState : EnemyState
{
    private Rigidbody m_Body;
    private RangedEnemyStateMachine m_RangedStateMachine;


    public ChasingEnemyState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        m_Body = stateMachine.GetComponent<Rigidbody>();
    }

    public override void ExecuteUpdate()
    {
        Vector3 directiontoplayer = m_StateMachine.m_Player.transform.position - m_StateMachine.transform.position;
        m_Body.velocity = directiontoplayer.normalized * 3;

        if ((m_StateMachine.transform.position - m_StateMachine.m_Player.GetPlayerPosition).magnitude > 5)
        {
            m_StateMachine.ChangeState(new IdleEnemyState(m_StateMachine));
        }
    }

    public override void ExecuteFixedUpdate()
    {
    }

    public override void ExecuteOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ApplyDamage(1);
        }
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
