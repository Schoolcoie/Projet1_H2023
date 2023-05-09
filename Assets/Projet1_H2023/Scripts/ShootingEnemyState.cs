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
        if (!m_RangedStateMachine.oncooldown) //also check for obstruction
        {
            m_RangedStateMachine.InstanciateBullet(); 
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
