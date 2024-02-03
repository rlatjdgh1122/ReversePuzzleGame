using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [Header("�÷��̾� �� �Ʒ� ����")]
    [Range(30,85)] public int AngleXUpLimit = 80;
    [Range(30,85)] public int AngleXDownLimit = 45;
    [Header("�÷��̾� ȭ�� ����")]
    [SerializeField] private float sensitivity = 2.0f; // ���콺 ����
    [SerializeField] private Transform _playerTrm;

    private float _eulerAngleX = 0;
    private float _eulerAngleY = 0;

    public void OnRotateEvent(float mouseX, float mouseY)
    {
        _eulerAngleX -= mouseY * sensitivity;
        _eulerAngleY += mouseX * sensitivity;

        _eulerAngleX = ClampAngle(_eulerAngleX);

        _playerTrm.localRotation = Quaternion.Euler(0, _eulerAngleY, 0); //�÷��̾�� �¿�
        transform.localRotation = Quaternion.Euler(_eulerAngleX, 0, 0); //ī�޶� ����
    }

    private float ClampAngle(float angle)
    {
        if (angle < -360) angle += 360f;
        if (angle > 360) angle -= 360f;

        return Mathf.Clamp(angle, -AngleXUpLimit, AngleXDownLimit);
    }
}
