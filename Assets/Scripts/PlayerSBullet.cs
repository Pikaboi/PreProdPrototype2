using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSBullet : Bullet
{
    //Shotgun Bullets
    //Stronger but Slower
    //Can Richochet

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_timer = 5.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (m_timer == 5.0f)
        {
            m_Vel = m_rb.velocity;
        }
        base.Update();
    }

    public override void Fire(Vector3 _Direction, float _playerSpeed, float _dir)
    {
        //The direction
        base.Fire(_Direction, _playerSpeed, _dir);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            m_rb.AddForce(Vector3.Reflect(m_rb.velocity, transform.right), ForceMode.Impulse);
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

}
