using UnityEngine;

public class CacaThrower : MonoBehaviour
{
    [SerializeField] private GameObject _cacaPrefab;
    [SerializeField] private float _maxTime = 2.0f;
    private float _time = 0.0f;
    private Transform _playerTr;

    private void TryThrowShit()
    {
        if (_time <= 0.0f)
        {
            GameObject cacaBullet = Instantiate(_cacaPrefab, transform.position, Quaternion.identity);
            cacaBullet.GetComponent<CacaComponent>().Damage = GetComponent<Enemy>()._damage*5;
            _time = _maxTime;
        }
        else
        {
            _time -= Time.deltaTime;
        }
    }

    void Start()
    {
        _playerTr = GameManager.Instance.GetPlayer().transform;
    }

    void Update()
    {
        if (Vector3.Distance(_playerTr.position, transform.position) <= 10.0f)
        {
            TryThrowShit();
        }
    }
}
