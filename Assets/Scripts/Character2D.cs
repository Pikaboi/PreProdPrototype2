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

    public float m_VibeTimer = 30.0f;

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
        VibeControl();
    }

    void CheckGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundChecker.transform.position, m_groundDistance, m_groundMask);

        if (!m_isGrounded)
        {
            if (m_rb.velocity.y > 0)
            {
                m_rb.AddForce(Physics.gravity, ForceMode.Acceleration);
            }

            if (m_rb.velocity.y < 0)
            {
                m_rb.AddForce(Physics.gravity * 3.0f, ForceMode.Acceleration);
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

    void VibeControl()
    {
        if (m_vibin)
        {
            //So it decreases in real time
            m_VibeTimer -= Time.unscaledDeltaTime;
        } else
        {
            m_VibeTimer += Time.unscaledDeltaTime;
        }

        m_VibeTimer = Mathf.Clamp(m_VibeTimer, 0.0f, 30.0f);

        if(m_VibeTimer <= 0.0f && m_vibin)
        {
            m_vibin = !m_vibin;
            Debug.Log(m_vibin);
            Time.timeScale = m_vibin ? 0.1f : 1.0f;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ladder")
        {
            float y = Input.GetAxisRaw("Vertical");
            m_rb.velocity += new Vector3(0.0f, y, 0.0f);
        }
    }

    public float GetHealth()
    {
        return m_health;
    }
}
