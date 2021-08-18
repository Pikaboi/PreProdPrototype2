using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_rb;
    public float m_Speed;
    public Vector3 m_Direction;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Fire(Vector3 _Direction)
    {
        m_rb.AddForce(_Direction, ForceMode.Impulse);
        m_rb.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
    }
}
