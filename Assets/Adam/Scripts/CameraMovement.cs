using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform m_CamPosition;

    private void Update()
    {
        transform.position = m_CamPosition.position;
    }
}
