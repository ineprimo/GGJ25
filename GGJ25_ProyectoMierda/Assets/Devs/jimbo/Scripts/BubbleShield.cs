using System.Linq;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;
    
    [SerializeField] private float _distanceArea = 5.0f;
    [SerializeField] private float _freezeTime = 3.0f;
    [SerializeField] private float _cooldown = 30.0f;
    private float _cd;
    private bool _froze;

    private GameObject _enemy;

    private void OnCollisionEnter(Collision collision)
    {
        _enemy = collision.gameObject;

        if (!(_cd <= 0.0f) || !_enemy.TryGetComponent(out CaquitaMovement _)) return;
        
        Freeze();
        _cd = _cooldown;
    }

    private void Freeze()
    {
        foreach (GameObject g in GameManager.Instance.SceneEnemies.Where(
                     g => (g.transform.position - gameObject.transform.position).magnitude < _distanceArea
                     ))
        {
            g.GetComponent<CaquitaMovement>().enabled = false;
            Instantiate(_bubble, g.transform.position, Quaternion.identity).transform.localScale =
                new Vector3(3, 3, 3);
            _froze = true;
        }
    }

    private void UnFreeze()
    {
        foreach (GameObject g in GameManager.Instance.SceneEnemies.Where(
                     g => !g.GetComponent<CaquitaMovement>().enabled
                 ))
        {
            g.GetComponent<CaquitaMovement>().enabled = true;
        }
    }

    public void UpdateAbility(float cdReduced)
    {
        _cooldown -= cdReduced;
    }
    
    private void Update()
    {
        _cd -= Time.deltaTime;
        if (_froze) _freezeTime -= Time.deltaTime;

        if (!(_freezeTime <= 0.0f)) return;
        UnFreeze();
        _freezeTime = 3.0f;
    }
}
