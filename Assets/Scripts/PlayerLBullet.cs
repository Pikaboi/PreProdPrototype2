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
        m_timer = 5.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Fire(Vector3 _Direction, float _playerSpeed, float _dir)
    {
        //The direction
        m_rb.velocity = _Direction * m_Speed;
        //Add the players direction
        //m_rb.AddForce(_dir * transform.right * _playerSpeed, ForceMode.Impulse);
    }
}