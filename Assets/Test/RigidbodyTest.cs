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
<<<<<<< Updated upstream
        //이해를 돕기 위해서 Vector3.right 사용

        Debug.Log("R : " + transform.right + " : " + Vector3.right);
        Debug.Log("F : " + transform.forward + " : " + Vector3.forward);
        if (Input.GetKeyDown(KeyCode.W))
            transform.rotation *= Quaternion.AngleAxis(-90, Vector3.right); //앞
        else if (Input.GetKeyDown(KeyCode.A))
            transform.rotation *= Quaternion.AngleAxis(-90, Vector3.forward); //왼
        else if (Input.GetKeyDown(KeyCode.S))
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.right); //뒤
        else if (Input.GetKeyDown(KeyCode.D))
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.forward); //오
=======
        if (Input.GetKeyDown(KeyCode.E))
            Physics.gravity = new Vector2(9.8f, 0);
>>>>>>> Stashed changes
    }
}
