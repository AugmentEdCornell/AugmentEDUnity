using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private GameObject m_HoverMessage;
    [SerializeField] private Color m_HoverColor = Color.green;

    [SerializeField] private bool m_ZoomOnClick;
    [SerializeField] private float m_ZoomDistance = 1f;
    [SerializeField] private float m_ZoomSpeed = 3f;
    [SerializeField] private GameObject m_ClickMessage;
    [SerializeField] private Color m_ClickColor = Color.blue;

    [SerializeField] private ClickableController m_Controller;
    
    // show outline when hovered, change outline color when clicked
    private Outline m_OutlineScript;
    private bool m_Selected;

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    
    private void Start()
    {
        m_OutlineScript = GetComponent<Outline>();
        Reset();
        //StartCoroutine(DisableOutlineAfterDelay());
    }

    // zoom to object when clicked
    private void OnMouseDown()
    {
        if (!IsPointerOverUI() && m_Controller.allowClick) OnClick();
    }

    public void OnClick()
    {
        if (m_ZoomOnClick)
        {
            Camera.main.GetComponent<MouseOrbitSample>()
                .RefreshCameraFixedRotation(transform, m_ZoomDistance, m_ZoomSpeed);
        }
        if (true)
        {
            m_Selected = !m_Selected;
            if (m_ClickMessage) m_ClickMessage.SetActive(m_Selected);
            if (m_Selected)
            {
                // turn on outline
                TurnOnOutline(m_ClickColor);
            }
        }
    }

    // show hover message when hover
    private void OnMouseEnter()
    {
        if (!IsPointerOverUI() && m_Controller.allowClick) OnHover();
    }

    public void OnHover()
    {
        if (m_HoverMessage && !m_ClickMessage.activeSelf) m_HoverMessage.SetActive(true);
        // show outline if hovered and not selected
        if (m_OutlineScript && !m_Selected)
        {
            TurnOnOutline(m_HoverColor);
        }
    }

    // hides hover message when exit
    private void OnMouseExit()
    {
        OnExit();
    }

    public void OnExit()
    {
        if (m_HoverMessage) m_HoverMessage.SetActive(false);
        // hide outline if its not selected
        if (m_OutlineScript && !m_Selected) m_OutlineScript.enabled = false;
    }

    // resets the stage to hide messages
    public void Reset()
    {
        if (m_HoverMessage) m_HoverMessage.SetActive(false);
        if (m_ClickMessage) m_ClickMessage.SetActive(false);
        if (m_OutlineScript) m_OutlineScript.enabled = false;
        m_Selected = false;
    }

    IEnumerator DisableOutlineAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        if (m_OutlineScript) m_OutlineScript.enabled = false;
    }

    private void TurnOnOutline(Color color)
    {
        m_OutlineScript.enabled = true;
        m_OutlineScript.OutlineColor = color;
    }
    
    
}
