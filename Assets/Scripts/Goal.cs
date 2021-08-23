using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public LoadScene m_load;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //You win ig
            m_load.Win();
        }
    }
}
