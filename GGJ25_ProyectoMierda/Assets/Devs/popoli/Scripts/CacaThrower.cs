using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacaThrower : MonoBehaviour
{
    [SerializeField] private GameObject _caca;
    
    private CaquitaMovement _move;
    private Transform _tr;
    private Transform _playerTr;
    private Transform _cacaSpawn;
    
    [SerializeField] private float _maxTime = 2.0f;
    private float _time = 0.0f;

    private void TryThrowShit()
    {
        if(_time <= 0.0f)
        {
            GameObject cacaBullet = Instantiate(_caca, _cacaSpawn);
            //cacaBullet.GetComponent<CacaComponent>().setDirection(   ); // auqi?¿?
            _time = _maxTime;
        }
        else _time -= Time.deltaTime;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponent<CaquitaMovement>();
        _tr = transform;
        _playerTr = GameManager.Instance.GetPlayer().transform;
        _cacaSpawn = _tr.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((_playerTr.position - _tr.position).magnitude <= 5.0f)
        {
            TryThrowShit();
        }
    }
}
