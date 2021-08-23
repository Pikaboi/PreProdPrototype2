using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibeTimerUI : MonoBehaviour
{
    Slider m_DebugVibe;
    Character2D m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_DebugVibe = GetComponent<Slider>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character2D>();

        m_DebugVibe.maxValue = m_player.m_VibeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        m_DebugVibe.value = m_player.m_VibeTimer;
    }
}
