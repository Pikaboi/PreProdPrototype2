using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_rb;
    public float m_Speed;
    public Vector3 m_Direction;
    public int Damage;
    public float m_ZoneTimeScale = 1.0f;

    public float m_timer = 0.5f;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        m_timer -= Time.deltaTime;

        if(m_timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Fire(Vector3 _Direction, float _playerSpeed, float _dir)
    {
        //The direction
        m_rb.AddForce(_Direction * m_Speed * m_ZoneTimeScale, ForceMode.Impulse);
        //Add the players direction
        m_rb.AddForce(_dir * transform.right * _playerSpeed, ForceMode.Impulse);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CustomTScale")
        {
            m_ZoneTimeScale = other.GetComponent<CustomTimeScale>().c_Time;
        }
    }
}
