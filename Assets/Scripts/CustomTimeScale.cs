using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimeScale : MonoBehaviour
{
    public float c_Time = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(c_Time == 1.0f)
            {
                c_Time = 0.1f;
            } else
            {
                c_Time = 1.0f;
            }
        }
    }
}
