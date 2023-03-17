using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToShow : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjectToShow;
    private Material m_DefaultMaterial;
    [SerializeField] private Material m_HoveredMaterial;
    [SerializeField] private MeshRenderer m_MeshHovered;

    private void OnMouseDown()
    {
        ToggleObject();
    }

    private void Start()
    {
        m_ObjectToShow.SetActive(false);
        m_DefaultMaterial = m_MeshHovered.material;
    }

    private void ToggleObject()
    {
        m_ObjectToShow.SetActive(!m_ObjectToShow.activeSelf);
    }

    private void OnMouseOver()
    {
        m_MeshHovered.material = m_HoveredMaterial;
    }

    private void OnMouseExit()
    {
        m_MeshHovered.material = m_DefaultMaterial;
    }
}
