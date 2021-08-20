using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshGrid : MonoBehaviour
{
    public AstarPath m_Astar;
    // Start is called before the first frame update
    void Start()
    {
        m_Astar = GetComponent<AstarPath>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Astar.Scan();
    }
}
