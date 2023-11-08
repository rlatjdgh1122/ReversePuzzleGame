using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyTest : MonoBehaviour
{
    [Range(0f, 30f)]
    public float Power = 15f;

    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //���ظ� ���� ���ؼ� Vector3.right ���
        if (Input.GetKeyDown(KeyCode.E))
            Physics.gravity = new Vector2(9.8f, 0);
        // _rb.AddForce(Vector3.right * Power, ForceMode.Impulse);
    }
}
