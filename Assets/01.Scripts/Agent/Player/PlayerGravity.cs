using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float gravityPower;


    [SerializeField] private float rev_castingTime = 1f; //시전시간

    private Vector3 _curDir = Vector3.zero;
    private Vector3 _endDir = Vector3.zero;

    private Coroutine rotCoroutine = null;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = transform.root.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.useGravity = false;

        OnRegister();
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.AddForce(-transform.root.up * gravityPower, ForceMode.Acceleration); //가속하는 힘
        }
    }
    private void OnRegister()
    {
        SignalHub.OnJumpEventHandled += OnJump;
    }

    private void OnDestroy()
    {
        SignalHub.OnJumpEventHandled -= OnJump;
    }
    private void OnJump(float power)
    {
        _rb.AddForce(transform.root.up * (power), ForceMode.Impulse); // 순간적인 힘
    }

    public void OnPlayerReverseGravity(Vector3 value)
    {
        Vector3 getDir = GetReverseDiraction(value);

        if (rotCoroutine != null)
        {
            StopCoroutine(rotCoroutine);
        }
        rotCoroutine = StartCoroutine(RotateCoroutine(getDir));
    }

    private IEnumerator RotateCoroutine(Vector3 value)
    {
        float t = 0;
        Vector3 startDir = _curDir;
        while (t < rev_castingTime)
        {
            t += Time.deltaTime;

            _curDir = Vector3.Lerp(startDir, value, t / rev_castingTime);
            pivot.rotation = Quaternion.Euler(_curDir); //포지션 돌리기
            //pivot.Rotate(_curDir, Space.World);
            yield return null;
        }
        pivot.rotation = Quaternion.Euler(_curDir);
        //pivot.Rotate(_curDir, Space.World);
    }

    private Vector3 GetReverseDiraction(Vector3 value1)
    {
        _curDir = _endDir;

        Vector3 value = (value1 - transform.up).normalized;

        if (Mathf.Abs(value.x) > Mathf.Abs(value.z))
        {
            if (value.x > 0) //오른쪽
            {
                _endDir += new Vector3(0, 0, 90);
                Debug.Log("오");
            }
            else if (value.x < 0) //왼쪽
            {
                _endDir += new Vector3(0, 0, -90);
                Debug.Log("왼");
            }
        }

        else if (Mathf.Abs(value.x) < Mathf.Abs(value.z))
        {
            if (value.z > 0) //앞쪽
            {
                _endDir += new Vector3(-90, 0, 0);
                Debug.Log("앞");
            }
            else if (value.z < 0) //뒤쪽
            {
                _endDir += new Vector3(90, 0, 0);
                Debug.Log("뒤");
            }
        }
        else if (Mathf.Abs(value.x - value.z) <= 0) //가만히 있었을때
        {
            _endDir += new Vector3(180, 0, 0);
        }

        return _endDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        var pos = transform.TransformPoint(0, 0, 1f);
        Gizmos.DrawWireSphere(pos, .5f);

    }
}