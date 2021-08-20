using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] private GameObject m_Bullet;
    private GameObject m_currentBullet;

    [SerializeField] private float m_MaxShootTimer = 1.5f;

    private float m_shootTimer = 0.0f;

    private bool m_Patrolling = true;

    // Start is called before the first frame update
    public override void Start()
    {
        m_agent = null;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (m_Patrolling)
        {
            Patrolling();
        } else
        {
            Combat();
        }
    }

    private void Patrolling()
    {
        Collider[] collisions;
        collisions = Physics.OverlapSphere(transform.position, 20.0f);

        foreach (Collider c in collisions)
        {
            if (c.GetComponent<Character2D>() != null)
            {
                m_Patrolling = false;
                m_Player = c.gameObject;
            }
        }
    }

    private void Combat()
    {
        m_shootTimer -= Time.deltaTime;

        Lookat2D();

        if (m_currentBullet == null && m_shootTimer < 0.0f)
        {
            m_currentBullet = Instantiate(m_Bullet, transform.position + m_Aimer.transform.forward * 1.2f, transform.rotation);
            m_currentBullet.GetComponent<EnemyBullet>().Fire(m_Aimer.transform.forward, m_agent.speed, 1.0f);
            m_shootTimer = 1.5f;
        }
    }

}
