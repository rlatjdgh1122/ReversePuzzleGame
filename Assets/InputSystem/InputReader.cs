using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using static InputSystem;

public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;

    public event Action OnStartFireEvent;
    public event Action OnUpdateFireEvent;
    public event Action OnEndFireEvent;

    public Vector2 AimPosition { get; private set; }
    private InputSystem _playerInputAction; //�̱������� ����� �༮

    private bool isClicking = false;

    private void OnEnable()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new InputSystem();
            _playerInputAction.Player.SetCallbacks(this); //�÷��̾� ��ǲ�� �߻��ϸ� �� �ν��Ͻ��� ����
        }

        _playerInputAction.Player.Enable(); //Ȱ��ȭ
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }

    public void OnFire(InputAction.CallbackContext context) //���߿�
    {
        if (context.started)
        {
            // ���콺 ���� ��ư�� ó�� ������ �� ����˴ϴ�.
            isClicking = true;
            OnStartFireEvent?.Invoke();
        }
        else if (context.canceled)
        {
            // ���콺 ���� ��ư�� ������ �� ����˴ϴ�.
            isClicking = false;
            OnEndFireEvent?.Invoke();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }
    public void Update()
    {
        if (isClicking)
        {
            OnUpdateFireEvent?.Invoke();
        }
    }
}
