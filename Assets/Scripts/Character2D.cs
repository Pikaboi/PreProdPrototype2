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
    [SerializeField] private LineRenderer m_Line;
    [SerializeField] private AudioSource m_Damage;
    private bool m_isGrounded = false;

    Vector3 FireDirection = Vector3.zero;

    private RaycastHit hitInfo;

    [SerializeField] private GameObject m_BulletInstance;

    private PlayerWeapon m_WeaponControl;

    bool m_vibin = false;

    public float m_VibeTimer = 30.0f;

    float defaultFixedDeltaTime;

    private float m_ZoneTimeScale = 1.0f;

    GameObject[] uis;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_WeaponControl = GetComponent<PlayerWeapon>();
        defaultFixedDeltaTime = Time.fixedDeltaTime;

        uis = GameObject.FindGameObjectsWithTag("DeathUI");

        foreach (GameObject g in uis)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_health > 0)
        {
            CheckGrounded();
            ControlInputs();
            VibeControl();
        } else
        {
            Debug.Log(uis.Length);

            foreach(GameObject g in uis)
            {
                g.SetActive(true);
            }
        }
    }

    void CheckGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundChecker.transform.position, m_groundDistance, m_groundMask);

        if (!m_isGrounded)
        {
            if (m_rb.velocity.y > 0)
            {
                m_rb.AddForce(Physics.gravity, ForceMode.Acceleration);
                //m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y + ((Physics.gravity.y - 9.0f) * m_ZoneTimeScale), m_rb.velocity.z);
            }

            if (m_rb.velocity.y < 0)
            {
                m_rb.AddForce(Physics.gravity * 3.0f, ForceMode.Acceleration);
                //m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y + (Physics.gravity.y * m_ZoneTimeScale), m_rb.velocity.z);
            }
        }
    }

    void ControlInputs()
    {
        //Moving
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 mouseX = Input.mousePosition;

        mouseX = Camera.main.ScreenToWorldPoint(new Vector3(mouseX.x, mouseX.y, 4.041f));
        mouseX = new Vector3(mouseX.x, mouseX.y, transform.position.z);

        m_rb.velocity = new Vector3(x * m_speed * m_ZoneTimeScale, m_rb.velocity.y, m_rb.velocity.z);
        FireDirection = (mouseX - transform.position).normalized;

        //Debug.DrawLine(transform.position, transform.position + FireDirection * 10.0f, Color.red);
        m_Line.SetPosition(0, transform.position + FireDirection);
        m_Line.SetPosition(1, transform.position + FireDirection * 5.0f);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(new Vector3(0.0f, m_jump, 0.0f), ForceMode.Impulse);// * m_ZoneTimeScale
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

            GameObject[] timeScales = GameObject.FindGameObjectsWithTag("CustomTScale");

            foreach (GameObject g in timeScales)
            {
                if (g.GetComponent<CustomTimeScale>().c_Time == 1.0f)
                {
                    g.GetComponent<CustomTimeScale>().c_Time = 0.1f;
                }
                else if (g.GetComponent<CustomTimeScale>().c_Time == 0.1f)
                {
                    g.GetComponent<CustomTimeScale>().c_Time = 1.0f;
                }
            }
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

            GameObject[] timeScales = GameObject.FindGameObjectsWithTag("CustomTScale");

            foreach (GameObject g in timeScales)
            {
                if (g.GetComponent<CustomTimeScale>().c_Time == 1.0f)
                {
                    g.GetComponent<CustomTimeScale>().c_Time = 0.1f;
                }
                else if (g.GetComponent<CustomTimeScale>().c_Time == 0.1f)
                {
                    g.GetComponent<CustomTimeScale>().c_Time = 1.0f;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ladder")
        {
            float y = Input.GetAxisRaw("Vertical");
            m_rb.velocity += new Vector3(0.0f, y, 0.0f);
        }

        if (other.tag == "CustomTScale")
        {
            m_ZoneTimeScale = other.GetComponent<CustomTimeScale>().c_Time;
            m_Damage.pitch = m_ZoneTimeScale;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            m_rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public float GetHealth()
    {
        return m_health;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<EnemyBullet>() != null)
        {
            m_health--;
            m_Damage.Play();
        }
    }
}
