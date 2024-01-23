using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float gravityPower;

    [SerializeField] private float rev_castingTime = 1f; //시전시간

    private ConstantForce _gravityForce;

    private Vector3 _curDir = Vector3.zero;
    private Vector3 _endDir = Vector3.zero;

    private Coroutine rotCoroutine = null;
    private void Awake()
    {
        _gravityForce = transform.root.GetComponent<ConstantForce>();
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

            yield return null;
        }
        pivot.rotation = Quaternion.Euler(_curDir);
    }

    private Vector3 GetReverseDiraction(Vector3 value)
    {
        _curDir = _endDir;

        if (Mathf.Abs(value.x) > Mathf.Abs(value.z))
        {
            if (value.x > 0) //오른쪽
            {
                _endDir += new Vector3(0, 0, 90);
            }
            else if (value.x < 0) //왼쪽
            {
                _endDir += new Vector3(0, 0, -90);
            }
        }

        else if (Mathf.Abs(value.x) < Mathf.Abs(value.z))
        {
            if (value.z > 0) //앞쪽
            {
                _endDir += new Vector3(-90, 0, 0);
            }
            else if (value.z < 0) //뒤쪽
            {
                _endDir += new Vector3(90, 0, 0);
            }
        }
        else if (Mathf.Abs(value.x - value.z) <= 0) //가만히 있었을때
        {
            _endDir += new Vector3(180, 0, 0);
        }

        return _endDir;
    }
}
