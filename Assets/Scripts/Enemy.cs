using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent m_agent;
    public int m_Health;
    public GameObject m_Player;
    public GameObject m_Aimer;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void Lookat2D()
    {
        Vector3 lookat = m_Player.transform.position - transform.position;
        lookat.z = 0;
        lookat.y = 0;
        Quaternion Rotation = Quaternion.LookRotation(lookat);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 1);

        Vector3 aimerLook = m_Player.transform.position - m_Aimer.transform.position;
        aimerLook.z = 0;
        Quaternion aimerRot = Quaternion.LookRotation(aimerLook);
        m_Aimer.transform.rotation = Quaternion.Slerp(m_Aimer.transform.rotation, aimerRot, 0.2f);
    }
}
