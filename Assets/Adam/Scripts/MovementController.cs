using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_GroundDrag;
    
    [Header("GroundCheck")] 
    [SerializeField] private float m_PlayerHeight;
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_IsGrounded;
    
    
    [SerializeField] private Transform m_Orientation;
    private float m_HorizontalInput;
    private float m_VerticalInput;
    private Vector3 m_MoveDirection;
    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        ReadInput();
        GroundCheck();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ReadInput()
    {
        m_HorizontalInput = Input.GetAxisRaw("Horizontal");
        m_VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        m_MoveDirection = m_Orientation.forward * m_VerticalInput + m_Orientation.right * m_HorizontalInput;
        m_Rigidbody.AddForce(m_MoveDirection * m_MoveSpeed * 10f, ForceMode.Force);
    }

    private void GroundCheck()
    {
        m_IsGrounded = Physics.Raycast(transform.position, Vector3.down, m_PlayerHeight * 0.5f + 0.2f, m_WhatIsGround);
        if (m_IsGrounded) m_Rigidbody.drag = m_GroundDrag;
        else m_Rigidbody.drag = 0f;
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(m_Rigidbody.velocity.x, 0f, m_Rigidbody.velocity.z);
        if (flatVelocity.magnitude > m_MoveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * m_MoveSpeed;
            m_Rigidbody.velocity = new Vector3(limitedVelocity.x, 0f, limitedVelocity.z);
        }
    }
}
