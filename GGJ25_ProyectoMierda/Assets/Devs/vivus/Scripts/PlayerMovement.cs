using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _life = 50.0f;

    public void Move(Vector3 dir)
    {
        transform.position += dir * _speed * Time.deltaTime;
    }

    public void ImproveSpeed(float incr)
    {
        _speed += incr;
    }

    public void ReceiveDamage(float damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            // JUGADOR MUERE
            PlayerDies();
        }
    }

    void PlayerDies()
    {

    }
}
