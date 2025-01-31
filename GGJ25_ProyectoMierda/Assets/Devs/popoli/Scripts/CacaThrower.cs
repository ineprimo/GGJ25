using UnityEngine;

public class CacaThrower : MonoBehaviour
{
    [SerializeField] private GameObject _caca;
    [SerializeField] private float _maxTime = 2.0f;
    private float _time = 0.0f;
    private Transform _playerTr;

    private void TryThrowShit()
    {
        if (_time <= 0.0f)
        {
            // Instanciar proyectil en la misma posición que el enemigo, pero sin ser hijo de él
            GameObject cacaBullet = Instantiate(_caca, transform.position, Quaternion.identity);

            // Pasar el daño al proyectil
            cacaBullet.GetComponent<CacaComponent>().Damage = GetComponent<Enemy>()._damage;

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
        if (Vector3.Distance(_playerTr.position, transform.position) <= 5.0f)
        {
            TryThrowShit();
        }
    }
}
