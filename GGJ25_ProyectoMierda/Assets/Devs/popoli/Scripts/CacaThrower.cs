using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacaThrower : MonoBehaviour
{
    [SerializeField] private GameObject _caca;

    private Transform _playerTr;
    
    [SerializeField] private float _maxTime = 2.0f;
    private float _time = 0.0f;

    private void TryThrowShit()
    {
        if(_time <= 0.0f)
        {
            GameObject cacaBullet = Instantiate(_caca, transform);
            cacaBullet.GetComponent<CacaComponent>().Damage = transform.parent.gameObject.GetComponent<Enemy>()._damage;
            _time = _maxTime;
        }
        else _time -= Time.deltaTime;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTr = GameManager.Instance.GetPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_playerTr.position - transform.position).magnitude <= 5.0f)
        {
            TryThrowShit();
        }
    }
}
