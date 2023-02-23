using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToShow : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjectToShow;

    private void OnMouseDown()
    {
        ToggleObject();
    }

    private void Start()
    {
        m_ObjectToShow.SetActive(false);
    }

    private void ToggleObject()
    {
        m_ObjectToShow.SetActive(!m_ObjectToShow.activeSelf);
    }
}
