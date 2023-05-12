using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    protected EnemyState m_CurrentState;
    public Player m_Player;
    protected Vector3 m_SpawnPosition;
    protected float m_MaxHealth;
    protected float m_CurrentHealth;
    protected float m_ContactDamage;
    public bool m_CanChangeState;
    [SerializeField] protected GameObject m_TestHealth;

    public EnemyStateMachine()
    {
        m_MaxHealth = 10;
        m_ContactDamage = 3;
    }

    void Start()
    {
        Debug.Log("Starting");
        m_CanChangeState = true;
        m_CurrentHealth = m_MaxHealth;
        m_SpawnPosition = transform.position;
        m_CurrentState = new IdleEnemyState(this);
        m_Player = FindObjectOfType<Player>();
    }

    public void ChangeState(EnemyState newState)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_CurrentState = newState;
    }

    private void Update()
    {
        Debug.Log(m_CanChangeState);
        Debug.Log(m_CurrentState);
        m_CurrentState.ExecuteUpdate();
    }

    private void FixedUpdate()
    {
        m_CurrentState.ExecuteFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_CurrentState.ExecuteOnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        m_CurrentState.ExecuteOnCollisionExit(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CurrentState.ExecuteOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        m_CurrentState.ExecuteOnTriggerExit(other);
    }

    public void ApplyDamage(float Damage)
    {
        m_CurrentHealth -= Damage;

        m_TestHealth.transform.localScale = new Vector3(m_CurrentHealth / m_MaxHealth, m_TestHealth.transform.localScale.y, m_TestHealth.transform.localScale.z);

        if (m_CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (m_CurrentState is RoamingEnemyState)
        {
            if (this is RangedEnemyStateMachine)
            {
                ChangeState(new ShootingEnemyState(this));
                StartCoroutine(ChangeStateCooldown());
            }
            else
            {
                ChangeState(new ChasingEnemyState(this));
                StartCoroutine(ChangeStateCooldown());
            }
        }
    }

    private IEnumerator ChangeStateCooldown()
    {
        m_CanChangeState = false;
        yield return new WaitForSeconds(2);
        m_CanChangeState = true;
    }

}

