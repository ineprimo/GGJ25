using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _currentLife = 50.0f;
    [SerializeField] private float _maxLife = 50.0f;

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
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            PlayerDies();
        }
    }

    public void Heal(float incr)
    {
        if(_currentLife + incr >= _maxLife)
        {
            _currentLife = _maxLife;
        }
        _currentLife += incr;
    }

    public void ImproveMaxLife(float incr)
    {
        _maxLife += incr;
    }

    void PlayerDies()
    {
        // JUGADOR MUERE
    }
}
