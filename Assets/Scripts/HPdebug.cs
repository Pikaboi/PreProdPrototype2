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
        if(m_player.GetHealth() == m_health && m_player.GetHealth() > m_health * 0.9f)
        {
            m_Image.sprite = m_HPBar[0];
        } 
        else if (m_player.GetHealth() < m_health * 0.9f && m_player.GetHealth() > m_health * 0.8f)
        {
            m_Image.sprite = m_HPBar[1];
        } 
        else if (m_player.GetHealth() < m_health * 0.8f && m_player.GetHealth() > m_health * 0.7f)
        {
            m_Image.sprite = m_HPBar[2];
        }
        else if (m_player.GetHealth() < m_health * 0.7f && m_player.GetHealth() > m_health * 0.6f)
        {
            m_Image.sprite = m_HPBar[3];
        }
        else if (m_player.GetHealth() < m_health * 0.6f && m_player.GetHealth() > m_health * 0.5f)
        {
            m_Image.sprite = m_HPBar[4];
        }
        else if (m_player.GetHealth() < m_health * 0.5f && m_player.GetHealth() > m_health * 0.4f)
        {
            m_Image.sprite = m_HPBar[5];
        }
        else if (m_player.GetHealth() < m_health * 0.4f && m_player.GetHealth() > m_health * 0.3f)
        {
            m_Image.sprite = m_HPBar[6];
        }
        else if (m_player.GetHealth() < m_health * 0.3f && m_player.GetHealth() > m_health * 0.2f)
        {
            m_Image.sprite = m_HPBar[7];
        }
        else if (m_player.GetHealth() < m_health * 0.2f && m_player.GetHealth() > m_health * 0.1f)
        {
            m_Image.sprite = m_HPBar[8];
        }
        else if (m_player.GetHealth() < m_health * 0.1f && m_player.GetHealth() > m_health * 0.0f)
        {
            m_Image.sprite = m_HPBar[9];
        } else if(m_player.GetHealth() == 0)
        {
            m_Image.sprite = m_HPBar[10];
        }
    }
}
