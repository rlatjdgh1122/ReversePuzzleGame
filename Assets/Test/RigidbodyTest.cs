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
        //이해를 돕기 위해서 Vector3.right 사용
        if (Input.GetKeyDown(KeyCode.W))
            transform.rotation *= Quaternion.AngleAxis(-90, transform.right); //앞쪽
        else if (Input.GetKeyDown(KeyCode.A))
            transform.rotation *= Quaternion.AngleAxis(-90, transform.forward); //오른쪽
        else if (Input.GetKeyDown(KeyCode.S))
            transform.rotation *= Quaternion.AngleAxis(90, transform.right); //오른쪽
        else if(Input.GetKeyDown(KeyCode.D))
            transform.rotation *= Quaternion.AngleAxis(90, transform.forward); //오른쪽
    }
}
