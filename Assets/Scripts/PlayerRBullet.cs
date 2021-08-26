using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRBullet : Bullet
{
    //Revolver Bullets
    //Standard bullet type

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
        base.Fire(_Direction, _playerSpeed, _dir);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }
}
