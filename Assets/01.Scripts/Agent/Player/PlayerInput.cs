using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent<float, float> OnRotateEvent = null;
    [SerializeField] private UnityEvent<Vector3> OnMovementEvent = null;

    private void Update()
    {
        //������ ó��
        PlayerMovement();
        // ȸ�� ó��
        PlayerRotate();
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // �̵� ���� ����
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        OnMovementEvent?.Invoke(moveDirection);
    }
    private void PlayerRotate()
    {
        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");    

        OnRotateEvent?.Invoke(mouseX, mouseY);
    }
}
