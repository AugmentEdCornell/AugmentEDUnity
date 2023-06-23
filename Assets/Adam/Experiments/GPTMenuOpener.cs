using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject m_GPTMenu;

    public void HideGPT()
    {
        m_GPTMenu.SetActive(false);
    }

    public void ShowGPT()
    {
        m_GPTMenu.SetActive(true);
    }

    private void Start()
    {
        HideGPT();
    }
}
