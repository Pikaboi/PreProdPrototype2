using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDebug : MonoBehaviour
{
    public PlayerWeapon m_pw;

    public Image m_TextImage;
    public Sprite[] m_TextSprites;
    public Image m_WeaponImage;
    public Sprite[] m_WeaponSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_TextImage.sprite = m_TextSprites[(int)m_pw.m_BType];
        m_WeaponImage.sprite = m_WeaponSprites[(int)m_pw.m_BType];
    }
}
