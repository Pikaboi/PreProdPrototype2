using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    //Player Variables
    private Rigidbody m_rb;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_jump;
    [SerializeField] private float m_health;
    [SerializeField] private GameObject m_groundChecker;
    [SerializeField] private float m_groundDistance = 0.2f;
    [SerializeField] private LayerMask m_groundMask;
    private bool m_isGrounded = false;

    Vector3 FireDirection = Vector3.zero;

    private RaycastHit hitInfo;

    [SerializeField] private GameObject m_BulletInstance;

    private PlayerWeapon m_WeaponControl;

    bool m_vibin = false;

    float defaultFixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_WeaponControl = GetComponent<PlayerWeapon>();
        defaultFixedDeltaTime = Time.fixedDeltaTime;
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

        if (!m_isGrounded)
        {
            if (m_rb.velocity.y > 0)
            {
                m_rb.AddForce(Physics.gravity * Time.timeScale, ForceMode.Acceleration);
            }

            if (m_rb.velocity.y < 0)
            {
                m_rb.AddForce(Physics.gravity * 3.0f * Time.timeScale, ForceMode.Acceleration);
            }
        }
    }

    void ControlInputs()
    {
        //Moving
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        m_rb.velocity = new Vector3(x * m_speed * Time.fixedUnscaledDeltaTime / Time.fixedDeltaTime, m_rb.velocity.y, m_rb.velocity.z);
        FireDirection = new Vector3(x, y, 0.0f).normalized;

        Debug.DrawLine(transform.position, transform.position + FireDirection * 10.0f, Color.red);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            //m_rb.AddForce(new Vector3(0.0f, m_jump, 0.0f), ForceMode.Impulse);
            m_rb.velocity += new Vector3(0.0f, m_jump * Time.fixedUnscaledDeltaTime / Time.fixedDeltaTime, 0.0f);
        }

        //Stronger Gravity to maintain similar jump in bullet time
        if (m_vibin && !m_isGrounded)
        {
            m_rb.velocity += new Vector3(0.0f, (Physics.gravity.y * Time.timeScale) - 0.81f, 0.0f);
        }

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if(FireDirection == Vector3.zero)
            {
                FireDirection = new Vector3(1.0f, 0.0f, 0.0f);
            }

            m_WeaponControl.Shoot(FireDirection, m_speed, x);
        }

        //Swapping Weapons
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_WeaponControl.CycleWeapons();
        }

        //Vaporwave Vibe Mode
        if (Input.GetMouseButtonDown(1))
        {
            m_vibin = !m_vibin;
            Debug.Log(m_vibin);
            Time.timeScale = m_vibin ? 0.1f : 1.0f;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
    }
}
