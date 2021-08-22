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
        base.Update();
    }

    public override void Fire(Vector3 _Direction, float _playerSpeed, float _dir)
    {
        //The direction
        base.Fire(_Direction, _playerSpeed, _dir);
    }

}
