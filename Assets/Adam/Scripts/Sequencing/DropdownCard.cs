using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownCard : MonoBehaviour
{
    [SerializeField] private GameObject m_Card;
    private bool m_Opened;
    private void Start()
    {
        HideCard();
        
    }

    public void ToggleCard()
    {
        if (m_Opened) HideCard();
        else ShowCard();
    }

    private void ShowCard()
    {
        m_Card.SetActive(true);
        transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        m_Opened = true;
    }
    
    private void HideCard()
    {
        m_Card.SetActive(false);
        transform.rotation = Quaternion.identity;
        m_Opened = false;
    }
}
