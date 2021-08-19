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

    Vector3 FireDirection = Vector3.zero;

    private RaycastHit hitInfo;

    [SerializeField] private GameObject m_BulletInstance;

    private bool m_vibinMode = false;


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

        if (m_isGrounded)
        {
            if (m_rb.velocity.y > 0)
            {
                m_rb.AddForce(Physics.gravity * 5.0f, ForceMode.Acceleration);
            }

            if (m_rb.velocity.y < 0)
            {
                m_rb.AddForce(Physics.gravity * 15.0f, ForceMode.Acceleration);
            }
        }
    }

    void ControlInputs()
    {
        //Moving
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        m_rb.velocity = new Vector3(x * m_speed, m_rb.velocity.y, m_rb.velocity.z);
        FireDirection = new Vector3(x, y, 0.0f).normalized;

        Debug.DrawLine(transform.position, transform.position + FireDirection * 10.0f, Color.red);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(new Vector3(0.0f, m_jump, 0.0f), ForceMode.Impulse);
        }

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if(FireDirection == Vector3.zero)
            {
                FireDirection = new Vector3(1.0f, 0.0f, 0.0f);
            }

            //Fire a bullet
            GameObject newBullet = Instantiate(m_BulletInstance, transform.position + FireDirection * 1.2f, transform.rotation);
            newBullet.GetComponent<Bullet>().Fire(FireDirection, m_speed, x);
        }

        //Vaporwave Vibe Mode
        if (Input.GetMouseButton(1))
        {
            m_vibinMode = !m_vibinMode;
            Time.timeScale = m_vibinMode ? 0.5f: 1.0f;
        }
    }
}
