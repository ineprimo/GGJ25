using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;

    public void Move(Vector3 dir)
    {
        transform.position += dir * _speed * Time.deltaTime;
    }
}
