using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public enum BulletType
    {
        REVOLVER,
        SHOTGUN,
        LASER
    }

    int enumcount = 2;

    public BulletType m_BType;

    public GameObject[] m_bullets;
    public GameObject m_CurrentBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CycleWeapons()
    {
        m_BType++;

        if((int)m_BType > enumcount)
        {
            m_BType = 0;
        }

        switch (m_BType)
        {
            case BulletType.REVOLVER:
                m_CurrentBullet = m_bullets[0];
                break;
            case BulletType.SHOTGUN:
                m_CurrentBullet = m_bullets[1];
                break;
            case BulletType.LASER:
                m_CurrentBullet = m_bullets[2];
                break;
        }
    }

    public void Shoot(Vector3 _FireDirection, float _speed, float _x)
    {
        //Fire a bullet
        GameObject newBullet = Instantiate(m_CurrentBullet, transform.position + _FireDirection * 1.2f, transform.rotation);
        newBullet.GetComponent<Bullet>().Fire(_FireDirection, _speed, _x);
    }
}
