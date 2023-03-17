using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_SensitivityX;
    [SerializeField] private float m_SensitivityY;
    [SerializeField] private Transform m_CamTransform;
    private float m_XRotation;
    private float m_YRotation;

    private void Start()
    {
        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * m_SensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * m_SensitivityY;

        m_YRotation += mouseX;
        m_XRotation -= mouseY;
        m_XRotation = Mathf.Clamp(m_XRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(m_XRotation, m_YRotation, 0f);
        m_CamTransform.rotation = Quaternion.Euler(0f, m_YRotation, 0f);
    }
}
