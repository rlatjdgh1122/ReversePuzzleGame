using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float MoveSpeed = 3;

    public void OnMovementEvent(Vector3 value)
    {
        transform.Translate(value * MoveSpeed * Time.deltaTime,Space.Self);
    }
}
