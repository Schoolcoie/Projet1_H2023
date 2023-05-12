using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine m_StateMachine;

    public EnemyState(EnemyStateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
    }

    public abstract void ExecuteUpdate();
    public abstract void ExecuteFixedUpdate();
    public abstract void ExecuteOnCollisionEnter(Collision collision);
    public abstract void ExecuteOnCollisionExit(Collision collision);
    public abstract void ExecuteOnTriggerEnter(Collider other);
    public abstract void ExecuteOnTriggerExit(Collider other);
}
