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

    private Pathfinding.AIDestinationSetter m_DestinationControl;
    private Pathfinding.AIPath m_PathControl;

    private float m_speed = 5.0f;

    // Start is called before the first frame update
    public override void Start()
    {
        m_agent = null;
        m_PathControl = GetComponent<Pathfinding.AIPath>();
        m_DestinationControl = GetComponent<Pathfinding.AIDestinationSetter>();
    }

    // Update is called once per frame
    public override void Update()
    {
        m_PathControl.maxSpeed = m_speed * m_ZoneTimeScale;
        m_PathControl.repathRate = 1 / m_ZoneTimeScale;

        if (m_Patrolling)
        {
            Patrolling();
        } else
        {
            Combat();
        }

        base.Update();
    }

    private void Patrolling()
    {
        Collider[] collisions;
        collisions = Physics.OverlapSphere(transform.position, 10.0f);

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

        float posToPlayer;
        if (m_Player.transform.position.x - transform.position.x < 0.0f)
        {
            posToPlayer = 1.0f;
        }
        else
        {
            posToPlayer = -1.0f;
        }

        //Beyond Inelegant i'll fix it in Vert Slice
        m_DestinationControl.target.gameObject.transform.position = m_Player.transform.position + new Vector3(5.0f * posToPlayer, 5.0f, 0.0f);

        if (m_currentBullet == null && m_shootTimer < 0.0f)
        {
            m_currentBullet = Instantiate(m_Bullet, transform.position + m_Aimer.transform.forward, transform.rotation);
            //Make the _PlayerSpeed 2x the speed of the AIs speed
            m_currentBullet.GetComponent<EnemyBullet>().Fire(m_Aimer.transform.forward, m_PathControl.maxSpeed * 2.0f, 1.0f);
            m_shootTimer = 1.5f;
        }
    }
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

}
