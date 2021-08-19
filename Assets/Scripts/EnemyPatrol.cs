using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy
{
    [SerializeField] private Transform m_patrol;
    [SerializeField] private Vector3 m_ogPos;
    [SerializeField] private GameObject m_Bullet;

    private GameObject m_currentBullet;

    private bool m_Patrolling = true;

    private Vector3 m_currentDest;

    public override void Start()
    {
        base.Start();

        m_ogPos = transform.position;
        m_agent.SetDestination(m_patrol.position);
        m_currentDest = m_patrol.position;
    }

    // Update is called once per frame
    override public void Update()
    {
        if (m_Patrolling)
        {
            Patrol();
        } else
        {
            Combat();
        }
    }

    private void Patrol()
    {
        if (m_agent.remainingDistance == 0)
        {
            if (m_currentDest == m_patrol.position)
            {
                m_currentDest = m_ogPos;
                m_agent.destination = m_ogPos;
            }
            else if (m_currentDest == m_ogPos)
            {
                m_currentDest = m_patrol.position;
                m_agent.destination = m_patrol.position;
            }
        }

        Collider[] collisions;
        collisions = Physics.OverlapSphere(transform.position, 10.0f);

        foreach(Collider c in collisions)
        {
            if(c.GetComponent<Character2D>() != null)
            {
                m_Patrolling = false;
                m_Player = c.gameObject;
            }
        }

    }

    private void Combat()
    {
        Lookat2D();
        m_agent.SetDestination(m_Player.transform.position);

        if(m_currentBullet == null)
        {
            m_currentBullet = Instantiate(m_Bullet, transform.position + m_Aimer.transform.forward * 1.2f, transform.rotation);
            m_currentBullet.GetComponent<EnemyBullet>().Fire(m_Aimer.transform.forward, m_agent.speed, 1.0f);
        }

    }
}
