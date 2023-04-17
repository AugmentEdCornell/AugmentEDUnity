using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableController : MonoBehaviour
{
    public bool allowClick;
    [SerializeField] private MouseOrbitSample m_MouseOrbit;

    public void EnableClickOrbit()
    {
        allowClick = true;
        m_MouseOrbit.mouseRotateActive = true;
    }
    
    public void DisableClickOrbit()
    {
        allowClick = false;
        m_MouseOrbit.mouseRotateActive = false;
    }
}
