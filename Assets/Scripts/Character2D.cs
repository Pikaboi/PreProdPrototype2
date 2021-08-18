using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    //Player Variables
    private Rigidbody m_rb;
    [SerializeField] private float m_speed;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        m_rb.velocity = new Vector3(x * m_speed, m_rb.velocity.y, m_rb.velocity.z);
    }
}
