using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLBullet : Bullet
{
    //Laser Bullets
    //Really Weak and Rapid Fire
    //Lasers Ignore the time freeze

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (m_timer == 0.2f)
        {
            m_Vel = m_rb.velocity;
        }

        base.Update();

        
    }

    public override void Fire(Vector3 _Direction, float _playerSpeed, float _dir)
    {
        //The direction
        m_rb.AddForce(_Direction * m_Speed, ForceMode.Impulse);
        //Add the players direction
        m_rb.AddForce(_dir * transform.right * _playerSpeed, ForceMode.Impulse);

        m_shootAudio.Play();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public override void OnTriggerStay(Collider other)
    {
        if (other.tag == "CustomTScale")
        {
            if(other.GetComponent<CustomTimeScale>().c_Time == 0.1f)
            {
                m_ZoneTimeScale = 1.0f;
            } else {
                m_ZoneTimeScale = 0.1f;
            }
        }

        m_shootAudio.pitch = m_ZoneTimeScale;
    }
}
