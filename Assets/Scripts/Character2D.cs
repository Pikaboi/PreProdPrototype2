using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    //Player Variables
    private Rigidbody m_rb;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_jump;
    [SerializeField] private GameObject m_groundChecker;
    [SerializeField] private float m_groundDistance = 0.2f;
    [SerializeField] private LayerMask m_groundMask;
    private bool m_isGrounded = false;

    private RaycastHit hitInfo;

    [SerializeField] private GameObject m_BulletInstance;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        ControlInputs();
    }

    void CheckGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundChecker.transform.position, m_groundDistance, m_groundMask);
    }

    void ControlInputs()
    {
        //Moving
        float x = Input.GetAxisRaw("Horizontal");

        m_rb.velocity = new Vector3(x * m_speed, m_rb.velocity.y, m_rb.velocity.z);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(new Vector3(0.0f, m_jump, 0.0f), ForceMode.Impulse);
        }

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            //Fire a bullet
            GameObject newBullet = Instantiate(m_BulletInstance, transform.position + transform.forward, transform.rotation);
            newBullet.GetComponent<Bullet>().Fire();
        }
    }
}
