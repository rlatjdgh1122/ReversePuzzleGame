using System;
using System.Security.Cryptography;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

// �����̴� �������� ȸ���ϴµ� 
// ���� �������� �ʴ´ٸ� �ݴ�� ȸ��

public class PlayerInput : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    [SerializeField] private KeyCode rev_key = KeyCode.G; //Ű
    [SerializeField] private float rev_coolTime = 3f; //��Ÿ��

    [Header("���� �̺�Ʈ")]
    [SerializeField] private UnityEvent<Vector3> OnReverseGravityEvent = null;
    [SerializeField] private UnityEvent<float, float> OnRotateEvent = null;
    [SerializeField] private UnityEvent<Vector3> OnMovementEvent = null;
    [SerializeField] private UnityEvent OnJumpEvent = null;

    #region ������Ƽ ����
    #endregion

    #region �Ϲ� ����

    private float rev_curTime = 0f;
    private Vector3 moveDirection = Vector3.zero;
    #endregion

    private void Update()
    {
        //���� ó��
        PlayerJump();
        //�÷��̾� �߷� ����
        PlayerReverseGravity();
        //������ ó��
        PlayerMovement();
        // ȸ�� ó��
        PlayerRotate();
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpEvent?.Invoke();
        }
    }

    private void PlayerReverseGravity()
    {
        rev_curTime += Time.deltaTime;
        if (rev_curTime > rev_coolTime
            && Input.GetKeyDown(rev_key))
        {
            OnReverseGravityEvent?.Invoke(moveDirection);

            rev_curTime = 0f;
        }
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // �̵� ���� ���� (ī�޶� ���� ����)
        moveDirection =
            (transform.right * horizontal
            + transform.forward * vertical).normalized;

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
