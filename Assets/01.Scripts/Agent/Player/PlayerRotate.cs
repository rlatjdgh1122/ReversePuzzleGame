using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [Header("플레이어 위 아래 조절")]
    [Range(30,85)] public int AngleXUpLimit = 80;
    [Range(30,85)] public int AngleXDownLimit = 45;
    [Header("플레이어 화면 감도")]
    [SerializeField] private float sensitivity = 2.0f; // 마우스 감도
    [SerializeField] private Transform _playerTrm;

    private float _eulerAngleX = 0;
    private float _eulerAngleY = 0;

    public void OnRotateEvent(float mouseX, float mouseY)
    {
        _eulerAngleX -= mouseY * sensitivity;
        _eulerAngleY += mouseX * sensitivity;

        _eulerAngleX = ClampAngle(_eulerAngleX);

        _playerTrm.localRotation = Quaternion.Euler(0, _eulerAngleY, 0); //플레이어는 좌우
        transform.localRotation = Quaternion.Euler(_eulerAngleX, 0, 0); //카메라만 상하
    }

    private float ClampAngle(float angle)
    {
        if (angle < -360) angle += 360f;
        if (angle > 360) angle -= 360f;

        return Mathf.Clamp(angle, -AngleXUpLimit, AngleXDownLimit);
    }
}
