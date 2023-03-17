using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private GameObject m_HoverMessage;
    [SerializeField] private bool m_ZoomOnClick;
    [SerializeField] private float m_ZoomDistance = 1f;
    [SerializeField] private float m_ZoomSpeed = 3f;
    [SerializeField] private GameObject m_ClickMessage;

    private void Start()
    {
        Reset();
    }

    // zoom to object when clicked
    private void OnMouseDown()
    {
        if (m_ZoomOnClick)
        {
            Camera.main.GetComponent<MouseOrbitSample>()
                .RefreshCameraFixedRotation(transform, m_ZoomDistance, m_ZoomSpeed);
        }
        if (m_ClickMessage) m_ClickMessage.SetActive(!m_ClickMessage.activeSelf);
    }

    // show hover message when hover
    private void OnMouseEnter()
    {
        if (m_HoverMessage && !m_ClickMessage.activeSelf) m_HoverMessage.SetActive(true);
    }

    // hides hover message when exit
    private void OnMouseExit()
    {
        if (m_HoverMessage) m_HoverMessage.SetActive(false);
    }

    // resets the stage to hide messages
    public void Reset()
    {
        if (m_HoverMessage) m_HoverMessage.SetActive(false);
        if (m_ClickMessage) m_ClickMessage.SetActive(false);
    }
}
