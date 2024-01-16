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
        //움직임 처리
        PlayerMovement();
        // 회전 처리
        PlayerRotate();
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 이동 방향 설정
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        OnMovementEvent?.Invoke(moveDirection);
    }
    private void PlayerRotate()
    {
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");    

        OnRotateEvent?.Invoke(mouseX, mouseY);
    }
}
