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

        Debug.Log("R : " + transform.right + " : " + Vector3.right);
        Debug.Log("F : " + transform.forward + " : " + Vector3.forward);
        if (Input.GetKeyDown(KeyCode.W))
            transform.rotation *= Quaternion.AngleAxis(-90, Vector3.right); //��
        else if (Input.GetKeyDown(KeyCode.A))
            transform.rotation *= Quaternion.AngleAxis(-90, Vector3.forward); //��
        else if (Input.GetKeyDown(KeyCode.S))
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.right); //��
        else if (Input.GetKeyDown(KeyCode.D))
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.forward); //��
    }
}
