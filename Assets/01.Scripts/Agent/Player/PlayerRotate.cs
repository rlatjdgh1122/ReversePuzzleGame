using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [Header("플레이어 화면 감도")]
    [SerializeField] private float sensitivity = 2.0f; // 마우스 감도

    private Transform rootTrm;
    private float eulerAngleX = 0;
    private float eulerAngleY = 0;

    private void Start()
    {
        rootTrm = transform.root;
    }
    public void OnRotateEvent(float mouseX, float mouseY)
    {
        eulerAngleX -= mouseY * sensitivity;
        eulerAngleY += mouseX * sensitivity;

        eulerAngleX = ClampAngle(eulerAngleX);

        //rootTrm.rotation = Quaternion.Euler(0, eulerAngleY, 0);
        rootTrm.rotation = Quaternion.AngleAxis(eulerAngleY, rootTrm.transform.up);
        transform.localRotation = Quaternion.Euler(eulerAngleX, 0, 0);
    }

    private float ClampAngle(float angle)
    {
        if (angle < -360) angle += 360f;
        if (angle > 360) angle -= 360f;

        return Mathf.Clamp(angle, -85, 85);
    }
}
