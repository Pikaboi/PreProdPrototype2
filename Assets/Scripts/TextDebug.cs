using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDebug : MonoBehaviour
{
    public PlayerWeapon m_pw;

    public Image m_Image;
    public Sprite[] m_Sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Image.sprite = m_Sprites[(int)m_pw.m_BType];
    }
}
