using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAroundObj : MonoBehaviour
{
    [SerializeField] private float m_Sensitivity = 0.4f;
    private Vector3 m_MouseLastFrame;
    [SerializeField] private bool m_IsRotating;
     
    void Start ()
    {
    }
     
    void Update()
    {
        if(m_IsRotating)
        {
            // offset
            Vector3 mouseOffset = (Input.mousePosition - m_MouseLastFrame);
            // apply rotation
            Vector3 rotation = Vector3.zero;
            //_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            rotation.y = -(mouseOffset.x) * m_Sensitivity;
            rotation.x = -(mouseOffset.y) * m_Sensitivity;
            // rotate
            //transform.Rotate(_rotation);
            transform.eulerAngles += rotation;
            Debug.Log(rotation);
            // store mouse
            m_MouseLastFrame = Input.mousePosition;
        }
    }
     
    void OnMouseDown()
    {
        // rotating flag
        m_IsRotating = true;
         
        // store mouse
        m_MouseLastFrame = Input.mousePosition;
    }
     
    void OnMouseUp()
    {
        // rotating flag
        m_IsRotating = false;
    }
}
