using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform m_MainCameraTransform;
    private void Start()
    {
        m_MainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - m_MainCameraTransform.position);
    }
}
