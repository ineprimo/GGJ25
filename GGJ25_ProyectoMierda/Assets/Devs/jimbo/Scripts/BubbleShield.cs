using System.Linq;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;
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
        _cd = 0; // Asegurar que el cooldown inicie en 0 para poder usar la habilidad al comienzo
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (_cd > 0) return; // No activar si la habilidad está en cooldown

        _enemy = other.gameObject;

        if (!_enemy.TryGetComponent(out CaquitaMovement _)) return;
        Freeze();
        _cd = _cooldown; // Iniciar cooldown después de usar la habilidad
    }

    private void Freeze()
    {
        foreach (GameObject g in GameManager.Instance.SceneEnemies.Where(
                     g => (g.transform.position - gameObject.transform.position).magnitude < _distanceArea
                 ))
        {
            Enemy enemyScript = g.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            enemyScript.Freeze();

            // Instancia la burbuja visual
            Instantiate(_bubble, g.transform).transform.localScale = new Vector3(3, 3, 3);

            // Detener Animator del enemigo y de sus dos primeros hijos
            StopAnimators(g);

            // Cambiar el color del material del enemigo a azul
            ChangeMaterialColor(g, Color.blue, 1f);

            _active = true;
        }
    }

    private void StopAnimators(GameObject enemy)
    {
        Animator mainAnimator = enemy.GetComponent<Animator>();
        if (mainAnimator != null) mainAnimator.enabled = false;

        // Detener los animators de los dos primeros hijos
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

        // Reanudar los animators de los dos primeros hijos
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

            // Reanudar animators
            ResumeAnimators(g);

            // Restaurar color original
            ChangeMaterialColor(g, Color.white, 1f);

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

        if (_cd > 0) _cd -= Time.deltaTime; // Reducir cooldown
        if (_active) _freezeTime -= Time.deltaTime;

        if (_freezeTime <= 0.0f)
        {
            UnFreeze();
            _freezeTime = 3.0f;
        }
    }
}
