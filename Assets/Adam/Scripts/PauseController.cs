using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    private bool m_IsPaused;
    public UnityEvent m_GamePaused;
    public UnityEvent m_GameResumed;

    [SerializeField] private GameObject m_PauseScreen;

    private void Start()
    {
        m_PauseScreen.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_IsPaused = !m_IsPaused;
            if (m_IsPaused)
            {
                Time.timeScale = 0f;
                m_PauseScreen.SetActive(true);
                m_GamePaused.Invoke();
            }
            else
            {
                Time.timeScale = 1f;
                m_PauseScreen.SetActive(false);
                m_GameResumed.Invoke();
            }
        }
    }
}
