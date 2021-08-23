using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPdebug : MonoBehaviour
{
    public Slider m_slider;
    public Character2D m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_slider.maxValue = m_player.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        m_slider.value = m_player.GetHealth();
    }
}
