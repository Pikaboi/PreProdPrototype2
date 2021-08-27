using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySentry : Enemy
{
    [SerializeField] private GameObject m_Bullet;
    private GameObject m_currentBullet;
    [SerializeField] private Animator m_animation;

    [SerializeField] private float m_MaxShootTimer = 3.0f;

    private float m_shootTimer = 0.0f;

    private bool m_Docile = true;

    private bool m_Triggered = false;

    public override void Start()
    {
        base.Start();
        m_shootTimer = m_MaxShootTimer;
    }

    // Update is called once per frame
    override public void Update()
    {
        if (m_Docile)
        {
            DetectPlayer();
        }
        else
        {
            Combat();
        }

        base.Update();
    }

    private void DetectPlayer()
    {
        Collider[] collisions;
        collisions = Physics.OverlapSphere(transform.position, 10.0f);

        foreach (Collider c in collisions)
        {
            if (c.GetComponent<Character2D>() != null)
            {
                m_Docile = false;
                m_Player = c.gameObject;
            }
        }

    }

    private void Combat()
    {
        m_shootTimer -= Time.deltaTime * m_ZoneTimeScale;

        Lookat2D();

        if(m_shootTimer < m_MaxShootTimer / 2 && !m_Triggered)
        {
            m_animation.SetTrigger("IsShooting");
            m_Triggered = true;
        }

        if (m_currentBullet == null && m_shootTimer < 0.0f)
        {
            m_currentBullet = Instantiate(m_Bullet, transform.position + m_Aimer.transform.forward, transform.rotation);
            m_currentBullet.GetComponent<EnemyBullet>().Fire(m_Aimer.transform.forward, m_agent.speed, 1.0f);
            m_shootTimer = m_MaxShootTimer;
            m_Triggered = false;
            //m_animation.SetTrigger("IsShooting");
        }

    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        m_animation.speed = m_ZoneTimeScale;
    }

}

