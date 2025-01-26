using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public AudioClip deathSound;
    private AudioSource audioSource;

    [SerializeField] private float _speed = 584.0f;
    [SerializeField] private float _currentLife = 1000.0f;
    [SerializeField] private float _maxLife = 1000.0f;
    [SerializeField] private int coins = 0;
    //[SerializeField] private float _fuerzaPaBajarAlPlayer = 10.0f;

    [SerializeField] private float _healTime = 5.0f;
    private float _healTimer;
    [SerializeField] private float _healPower = 1.0f;
    
    private Vector3 _dir;
    
    [SerializeField] private HUDController _hud;
    
    public float Health { get { return _currentLife;} }
    
    Rigidbody _rigidBody;



    // ANIMATIONS
    [SerializeField] GameObject _currentWeapon;

    [SerializeField] GameObject[] _weapons;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody>();
        coins = 0;
        
        _healTimer = _healTime;

        // cambia el current
        _currentWeapon = _weapons[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_healTimer <= 0)
        {
            Heal(_healPower);
        }
        else _healTimer -= Time.deltaTime;
    }

    public void Move(Vector3 dir)
    {
        dir.Normalize();
        _dir = dir;
        //_rigidBody.velocity = dir * (_speed * Time.deltaTime);
    }

    public void ImproveSpeed(float incr)
    {
        _speed += incr;
    }

    /// <summary>
    /// cambia al arma i que le pases 
    /// </summary>
    /// <param name="i"></param>
    public void changeWeapon(int i)
    {
        // desactiva el antiguo
        _currentWeapon.SetActive(false);
    }
    private void Hit(float damage)
    {
        _healTimer = _healTime;
        _currentLife -= damage;

        if (_maxLife * 0.75f >= _currentLife && _currentLife > _maxLife * 0.5f)
        {
            _hud.UpateSplash(1, true);
        }
        else if (_maxLife * 0.5f >= _currentLife && _currentLife > _maxLife * 0.25f)
        {
            _hud.UpateSplash(2, true);
        }
        else if (_maxLife * 0.25f >= _currentLife && _currentLife > _maxLife * 0.1f)
        {
            _hud.UpateSplash(3, true);
        }
        else if (_maxLife * 0.1f >= _currentLife)
        {
            _hud.UpateSplash(4, true);
        }
        if (_currentLife <= 0)
        {
            PlayerDies();
        }
    }

    private void Heal(float incr)
    {
        if(_currentLife + incr >= _maxLife)
        {
            _currentLife = _maxLife;
        }
        else _currentLife += incr;
        
        if (_maxLife * 0.1f <= _currentLife && _currentLife < _maxLife * 0.25f)
        {
            _hud.UpateSplash(4, false);
        }
        else if (_maxLife * 0.25f <= _currentLife && _currentLife < _maxLife * 0.5f)
        {
            _hud.UpateSplash(3, false);
        }
        else if (_maxLife * 0.5f <= _currentLife && _currentLife < _maxLife * 0.75f)
        {
            _hud.UpateSplash(2, false);
        }
        else if (_maxLife * 0.75f <= _currentLife)
        {
            _hud.UpateSplash(1, false);
        }
    }

    public void ImproveMaxLife(float incr)
    {
        _maxLife += incr;
    }

    void PlayerDies()
    {
        audioSource.PlayOneShot(deathSound);
        GameManager.Instance.EndGame();
    }

    public void addCoins(int nCoins)
    {
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
        else if (otherObject.layer == 9)
        {
            Hit(otherObject.GetComponent<Enemy>()._damage);
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(_dir.x * _speed, _rigidBody.velocity.y, _dir.z * _speed);
        _rigidBody.velocity = velocity * Time.fixedDeltaTime;
    }
}
