using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (m_timer == 0.5f)
        {
            m_Vel = m_rb.velocity;
        }

        base.Update();
    }

    public override void Fire(Vector3 _Direction, float _playerSpeed, float _dir)
    {
        //The direction
        m_rb.AddForce(_Direction * m_Speed * m_ZoneTimeScale, ForceMode.Impulse);
        m_shootAudio.Play();
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }
}
