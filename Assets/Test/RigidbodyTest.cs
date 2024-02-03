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
        if (Input.GetKeyDown(KeyCode.W))
            transform.rotation *= Quaternion.AngleAxis(-90, transform.right); //����
        else if (Input.GetKeyDown(KeyCode.A))
            transform.rotation *= Quaternion.AngleAxis(-90, transform.forward); //������
        else if (Input.GetKeyDown(KeyCode.S))
            transform.rotation *= Quaternion.AngleAxis(90, transform.right); //������
        else if(Input.GetKeyDown(KeyCode.D))
            transform.rotation *= Quaternion.AngleAxis(90, transform.forward); //������
    }
}
