using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _currentLife = 50.0f;
    [SerializeField] private float _maxLife = 50.0f;
    [SerializeField] private int coins = 0;
    
    [SerializeField] private LayerMask _caca;
    
    Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 dir)
    {
        dir.Normalize();
        _rigidBody.velocity = dir * _speed;
    }

    public void ImproveSpeed(float incr)
    {
        _speed += incr;
    }

    private void Hit(float damage)
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

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherObject = other.gameObject;
        
        if (otherObject.layer == _caca)
        {
            Hit(otherObject.GetComponent<CacaComponent>().Damage);
        }
    }
}
