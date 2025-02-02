using System.Linq;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    [SerializeField] private ParticleSystem _freezeEffect;
    [SerializeField] private AudioClip _freezeSound;
    [SerializeField] private float _distanceArea = 5.0f;
    [SerializeField] private float _freezeTime = 3.0f;
    [SerializeField] private float _cooldown = 30.0f;
    private float _cd;
    private bool _active;
    private GameObject _enemy;

    public float GetCd()
    {
        return _cd;
    }

    private void Start()
    {
        _cd = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (_cd > 0) return;

        _enemy = other.gameObject;

        if (!_enemy.TryGetComponent(out CaquitaMovement _)) return;
        Freeze();
        _cd = _cooldown;
    }

    private void Freeze()
    {
        foreach (GameObject g in GameManager.Instance.SceneEnemies.Where(
                     g => (g.transform.position - transform.position).magnitude < _distanceArea
                 ))
        {
            Enemy enemyScript = g.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            enemyScript.Freeze();

            
             _freezeEffect.Play();
            _freezeEffect.gameObject.GetComponent<AudioSource>().PlayOneShot(_freezeSound);
            
            Collider[] colliders = g.GetComponents<BoxCollider>();
            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                {
                    collider.enabled = false;
                    break;
                }
            }

            StopAnimators(g);
            ChangeMaterialColor(g, Color.blue, 1f);

            _active = true;
        }
    }

    private void StopAnimators(GameObject enemy)
    {
        Animator mainAnimator = enemy.GetComponent<Animator>();
        if (mainAnimator != null) mainAnimator.enabled = false;

        for (int i = 0; i < Mathf.Min(2, enemy.transform.childCount); i++)
        {
            Animator childAnimator = enemy.transform.GetChild(i).GetComponent<Animator>();
            if (childAnimator != null) childAnimator.enabled = false;
        }
    }

    private void ResumeAnimators(GameObject enemy)
    {
        Animator mainAnimator = enemy.GetComponent<Animator>();
        if (mainAnimator != null) mainAnimator.enabled = true;

        for (int i = 0; i < Mathf.Min(2, enemy.transform.childCount); i++)
        {
            Animator childAnimator = enemy.transform.GetChild(i).GetComponent<Animator>();
            if (childAnimator != null) childAnimator.enabled = true;
        }
    }

    private void ChangeMaterialColor(GameObject enemy, Color color, float intensity)
    {
        Renderer renderer = enemy.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            mat.color = Color.Lerp(mat.color, color, intensity);
        }
    }

    private void UnFreeze()
    {
        foreach (GameObject g in GameManager.Instance.SceneEnemies.Where(
                     g => g.GetComponent<Enemy>().Frozen
                 ))
        {
            Enemy enemyScript = g.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            enemyScript.Unfreeze();
            ResumeAnimators(g);
            ChangeMaterialColor(g, Color.white, 1f);

            Collider[] colliders = g.GetComponents<BoxCollider>();
            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                {
                    collider.enabled = true;
                    break;
                }
            }

            _active = false;
        }
    }

    public void UpdateAbility(float cdReduced)
    {
        _cooldown /= cdReduced;
    }

    private void Update()
    {
        GameManager.Instance.AbilityHud();

        if (_cd > 0) _cd -= Time.deltaTime;
        if (_active) _freezeTime -= Time.deltaTime;

        if (_freezeTime <= 0.0f)
        {
            UnFreeze();
            _freezeTime = 3.0f;
        }
    }
}