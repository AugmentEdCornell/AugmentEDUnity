using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class TheAnimator : MonoBehaviour
{
    [SerializeField] private GameObject[] m_theElements;
    private int m_CurrentIdx;
    [SerializeField] private ClickableController m_ClickController;
    private int m_MaxIdx;

    private void Start()
    {
        // sets the max index
        m_CurrentIdx = 0;
        m_MaxIdx = m_theElements.Length - 1;
        
        HideAllElements();
        
        // start our custom sequence
        ThePlantCellSequence();
    }

    
    /// <summary>
    /// This is the custom plant cell sequence
    /// </summary>
    private void ThePlantCellSequence()
    {
        // don't all clicking and orbiting on start, have to follow the sequence of actions
        m_ClickController.DisableClickOrbit();
        // Start this animation
        StartAnimFromBegining();
    }

    public void StartAnimFromBegining()
    {
        m_CurrentIdx = 0;
        HideAllElements();
        ShowThisElement(m_CurrentIdx);
    }

    /// <summary>
    /// Hides the current element and shows the next element.
    /// If there exists a next element
    /// </summary>
    public void ToNextElement()
    {
        // progress only if there exists next element
        if (m_CurrentIdx + 1 <= m_MaxIdx)
        {
            // hide this element
            HideThisElement(m_CurrentIdx);
            m_CurrentIdx++;
            ShowThisElement(m_CurrentIdx);
        }
    }
    
    /// <summary>
    /// Go back to the previous element
    /// </summary>
    public void ToPreviousElement()
    {
        // progress only if there exists previous element
        if (m_CurrentIdx - 1 >= 0)
        {
            // hide this element
            HideThisElement(m_CurrentIdx);
            m_CurrentIdx--;
            ShowThisElement(m_CurrentIdx);
        }
    }

    /// <summary>
    /// Hides all elements in the scene
    /// </summary>
    public void HideAllElements()
    {
        for (int i = 0; i < m_theElements.Length; i++)
        {
            HideThisElement(i);
        }
    }

    private void ShowThisElement(int i)
    {
        m_theElements[i].SetActive(true);
    }
    
    private void HideThisElement(int i)
    {
        m_theElements[i].SetActive(false);
    }
}
