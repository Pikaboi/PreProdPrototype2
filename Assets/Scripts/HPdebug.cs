using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPdebug : MonoBehaviour
{
    public Sprite[] m_HPBar;
    public Image m_Image;
    public Character2D m_player;
    float m_health;
    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        m_health = m_player.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //I hate this but idk a more elegant solution to what the artists made the health bar as
        if(m_player.GetHealth() == m_health)
        {
            m_Image.sprite = m_HPBar[0];
        } 
        else if (m_player.GetHealth() < m_health * 0.9)
        {
            m_Image.sprite = m_HPBar[1];
        } 
        else if (m_player.GetHealth() < m_health * 0.8)
        {
            m_Image.sprite = m_HPBar[2];
        }
        else if (m_player.GetHealth() < m_health * 0.7)
        {
            m_Image.sprite = m_HPBar[3];
        }
        else if (m_player.GetHealth() < m_health * 0.6)
        {
            m_Image.sprite = m_HPBar[4];
        }
        else if (m_player.GetHealth() < m_health * 0.5)
        {
            m_Image.sprite = m_HPBar[5];
        }
        else if (m_player.GetHealth() < m_health * 0.4)
        {
            m_Image.sprite = m_HPBar[6];
        }
        else if (m_player.GetHealth() < m_health * 0.3)
        {
            m_Image.sprite = m_HPBar[7];
        }
        else if (m_player.GetHealth() < m_health * 0.2)
        {
            m_Image.sprite = m_HPBar[8];
        }
        else if (m_player.GetHealth() < m_health * 0.1)
        {
            m_Image.sprite = m_HPBar[9];
        } else
        {
            m_Image.sprite = m_HPBar[10];
        }
    }
}
