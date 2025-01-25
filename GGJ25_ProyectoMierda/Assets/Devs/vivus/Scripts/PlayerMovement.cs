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
    
    [SerializeField] private HUDController _hud;
    
    public float Health { get { return _currentLife;} }
    
    Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        coins = 0;
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

        if (_maxLife * 0.75f >= _currentLife)
        {
            _hud.UpateSplash(1);
        }
        else if (_maxLife * 0.5f >= _currentLife)
        {
            _hud.UpateSplash(2);
        }
        else if (_maxLife * 0.25f >= _currentLife)
        {
            _hud.UpateSplash(3);
        }
        else if (_maxLife * 0.1f >= _currentLife)
        {
            _hud.UpateSplash(4);
        }
        else if (_currentLife <= 0)
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

    public void addCoins(int nCoins)
    {
        Debug.Log("A�ado " + nCoins + " moneda");
        coins += nCoins;
    }

    public void setCoins(int nCoins)
    {
        coins = nCoins;
    }

    public void subCoins(int nCoins)
    {
        coins -= nCoins;
    }

    public int GetCoins()
    {
        return coins;
    }



    private void OnCollisionEnter(Collision other)
    {
        GameObject otherObject = other.gameObject;
        
        if (otherObject.layer == 7)
        {
            Hit(otherObject.GetComponent<CacaComponent>().Damage);
            Destroy(otherObject);
        }
    }
}
