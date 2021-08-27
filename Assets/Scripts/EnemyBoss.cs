using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private Vector3 m_ogPos;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private Animator m_animation;

    private GameObject m_currentBullet;

    [SerializeField] private float m_MaxShootTimer = 5.0f;

    private float m_shootTimer = 0.0f;

    private bool m_Patrolling = true;

    private Vector3 m_currentDest;

    private float baseSpeed;

    private bool m_Triggered = false;

    public override void Start()
    {
        base.Start();

        m_ogPos = transform.position;

        m_shootTimer = m_MaxShootTimer;

        baseSpeed = m_agent.speed;
    }

    // Update is called once per frame
    override public void Update()
    {
        if (m_Patrolling)
        {
            Patrol();
        }
        else
        {
            Combat();
        }

        m_agent.velocity = m_agent.desiredVelocity * m_ZoneTimeScale;

        base.Update();
    }

    private void Patrol()
    {
        Collider[] collisions;
        collisions = Physics.OverlapSphere(transform.position, 10.0f);

        foreach (Collider c in collisions)
        {
            if (c.GetComponent<Character2D>() != null)
            {
                m_Patrolling = false;
                m_Player = c.gameObject;
                m_animation.SetBool("Patrol", false);
            }
        }

    }

    private void Combat()
    {
        m_shootTimer -= Time.deltaTime * m_ZoneTimeScale;

        Lookat2D();

        float posToPlayer;
        if (m_Player.transform.position.x - transform.position.x < 0.0f)
        {
            posToPlayer = 1.0f;
        }
        else
        {
            posToPlayer = -1.0f;
        }

        if (m_shootTimer < 1.0f && !m_Triggered)
        {
            m_animation.SetTrigger("Shoot");
            m_Triggered = true;
        }

        m_agent.SetDestination(m_Player.transform.position + new Vector3(5.0f, 0.0f, 0.0f) * posToPlayer);

        if (m_currentBullet == null && m_shootTimer < 0.0f)
        {
            m_currentBullet = Instantiate(m_Bullet, transform.position + m_Aimer.transform.forward, transform.rotation);
            m_currentBullet.GetComponent<EnemyBullet>().Fire(m_Aimer.transform.forward, m_agent.speed, 1.0f);
            m_shootTimer = m_MaxShootTimer;
            m_Triggered = false;
        }

    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        m_animation.speed = m_ZoneTimeScale;
    }

}