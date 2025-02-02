using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip[] sonidoscuquis;

    private AudioSource audioSource;
    public float _damage = 10.0f;
    [SerializeField] private float _health;
    public float _currentHealth;
    [SerializeField] private GameObject coin;
    [SerializeField] private float threshold = 0.5f;
    [SerializeField] private SpriteRenderer _eyes;
    [SerializeField] private Sprite _eye1;
    [SerializeField] private Sprite _eye2;
    [SerializeField] private ParticleSystem splash;

    private const int SCORE_MELEE = 29;
    private const int SCORE_DISTANCE = 39;

    public void SetHealth(float h)
    {
        _health = h;
    }

    public bool Frozen { get; private set; } = false;

    public void Freeze()
    {
        Frozen = true;
        GetComponent<AIMovement>().enabled = false;
        if (GetComponent<CacaThrower>() != null) GetComponent<CacaThrower>().enabled = false;
    }

    public void Unfreeze()
    {
        Frozen = false;
        GetComponent<AIMovement>().enabled = true;
        if (GetComponent<CacaThrower>() != null) GetComponent<CacaThrower>().enabled = true;
    }
    
    // cuando la bala burbuja hittee al enemy 
    public void Hit(float damage)
    {
        _currentHealth -= damage;
        if (splash)
            splash.Play();

        if (_health * 0.5 >= _currentHealth && _currentHealth > _health * 0.1f)
        {
            _eyes.sprite = _eye1;

        }
        else if (_health * 0.1 >= _currentHealth)
        {
            if(_eye2 != null)
                _eyes.sprite = _eye2;
        }
        if (_currentHealth <= 0)
        {
            //Freeze();

            GetComponent<Animator>().SetTrigger("death");

            if (GetComponent<CacaThrower>())
            {
                GetComponent<CacaThrower>().enabled = false;
            }
            if (sonidoscuquis.Length > 0)
            {
                int indice = UnityEngine.Random.Range(0, sonidoscuquis.Length);
                audioSource.PlayOneShot(sonidoscuquis[indice]);
            }

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            StartCoroutine(Death());
        }
    }

    public void Bomba()
    {
        float f = UnityEngine.Random.Range(0f, 1f);
        if (f < threshold)
        {
            for (int i = 0; i < 5; i++)
            {
                // Generar un desplazamiento aleatorio cerca de la posici�n del enemigo
                Vector3 randomOffset = new Vector3(
                    UnityEngine.Random.Range(-1f, 1f), // Desplazamiento en X
                    0f,                               // Mantener la misma altura (Y)
                    UnityEngine.Random.Range(-1f, 1f) // Desplazamiento en Z
                );

                // Instanciar la moneda en la posici�n del enemigo + desplazamiento aleatorio
                Instantiate(coin, transform.position + randomOffset, transform.rotation);
            }
        }

        GameManager.Instance.deRegisterEnemy(gameObject);
        Destroy(gameObject);

        if (gameObject.GetComponent<CacaThrower>() != null)
            GameManager.Instance.increaseScore(SCORE_DISTANCE);
        else
            GameManager.Instance.increaseScore(SCORE_MELEE);
    }

    private IEnumerator Death()
    {
        Freeze();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        transform.localScale *= 0.25f;

        // Desactiva todos los colliders del GameObject
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(3);

        GetComponent<Animator>().SetTrigger("confetti");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            Hit(other.gameObject.GetComponent<Bullet>().Damage + GameManager.Instance._actualExtraDmg);
            Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        _currentHealth = _health;
        audioSource = GetComponent<AudioSource>();
    }
}
