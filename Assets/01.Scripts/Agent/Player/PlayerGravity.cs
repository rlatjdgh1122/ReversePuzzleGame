using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform pivot2;
    [SerializeField] private float gravityPower;

    [SerializeField] private float rev_castingTime = 1f;

    private Coroutine rotCoroutine = null;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = transform.root.GetComponent<Rigidbody>();

        OnRegister();
    }


    private void Start()
    {
        _rb.useGravity = false;
    }

    private void OnRegister()
    {
        SignalHub.OnJumpEventHandled += OnJump;
    }

    private void OnDestroy()
    {
        SignalHub.OnJumpEventHandled -= OnJump;
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.AddForce(-transform.root.up * gravityPower, ForceMode.Acceleration);
        }
    }
    private void OnJump(float power)
    {
        _rb.AddForce(transform.root.up * (power), ForceMode.Impulse); // 순간적인 힘
    }
    public void OnPlayerReverseGravity(KeyCode value)
    {
        Quaternion getDir = GetReverseDirection(value);

        if (rotCoroutine != null)
        {
            StopCoroutine(rotCoroutine);
        }
        rotCoroutine = StartCoroutine(RotateCoroutine(getDir));
    }

    private IEnumerator RotateCoroutine(Quaternion endRotation)
    {
        Quaternion startRotation = pivot.rotation;
        float t = 0;

        while (t < rev_castingTime)
        {
            t += Time.deltaTime;
            float progress = t / rev_castingTime;

            pivot.rotation = Quaternion.Slerp(startRotation, endRotation, progress);
            yield return null;
        }

        pivot.rotation = endRotation;
    }

    private Quaternion GetReverseDirection(KeyCode value)
    {
        Vector3Int getRotDir = GetRotationDirection();

        Quaternion targetRotation = pivot.rotation;
        pivot.localRotation = Quaternion.Euler(getRotDir);
        transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y - pivot.localEulerAngles.y, 0);


        Vector3 localRight = Vector3.right;
        Vector3 localForward = Vector3.forward;
        if (value == KeyCode.W) //앞
        {
            targetRotation *= Quaternion.AngleAxis(-90, localRight);
        }
        else if (value == KeyCode.A) //왼
        {
            targetRotation *= Quaternion.AngleAxis(-90, localForward);
        }
        else if (value == KeyCode.S) //뒤
        {
            targetRotation *= Quaternion.AngleAxis(90, localRight);
        }
        else if (value == KeyCode.D) //오
        {
            targetRotation *= Quaternion.AngleAxis(90, localForward);
        }

        else if (value == KeyCode.None) //가만히 있을때
        {
            targetRotation *= Quaternion.AngleAxis(180, Vector3.right);
        }

        return targetRotation;
    }

    private List<int> values = new List<int>()
    {0, 90, 180, 270, 360 };
    private Vector3Int GetRotationDirection()
    {
        int playerY = (int)transform.localRotation.eulerAngles.y;
        if (playerY < 0)
        {
            playerY = 360 - playerY;
        }
        int value = values.OrderBy(x => Mathf.Abs(playerY - x)).First();

        if (value == 360)
            value = 0;

        Debug.Log("playerY : " + playerY);
        Debug.Log("value : " + value);

        return new Vector3Int(0, value, 0);
    }
}