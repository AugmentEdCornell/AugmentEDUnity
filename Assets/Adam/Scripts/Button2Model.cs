using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button2Model : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ClickableObject m_Model;
    private Button m_Button;
    //private EventTrigger m_EventTrigger;

    private void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(m_Model.OnClick);
        //m_EventTrigger.OnPointerEnter;
        //m_EventTrigger.OnPointerEnter.(m_Model.OnHover);
        //m_EventTrigger.OnPointerExit(m_Model.OnExit);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Model.OnHover();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        m_Model.OnExit();
    }
}
