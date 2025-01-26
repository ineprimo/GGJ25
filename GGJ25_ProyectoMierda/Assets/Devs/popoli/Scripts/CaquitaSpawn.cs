using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CaquitaSpawn : MonoBehaviour
{
    public AudioClip[] sonidosSpawn;
    private AudioSource audioSource;
    private Transform _tr;

    [SerializeField] GameObject meleeEnemy;
    [SerializeField] GameObject meleeEnemy2;
    [SerializeField] GameObject throwerEnemy;

    private GameObject player;
    [SerializeField] float spawnTime = 3.0f; //seconds
    [SerializeField] float spawnDistance = 2.0f; // distancia para spawnear
    [SerializeField] float cacaThrowerDistance = 7.0f; // distancia para spawnear un caca thrower

    [SerializeField] float startTime = 5.0f; // tiempo para que empiece a spawnear

    Vector3 spawnPosition;
    [SerializeField] float spawnYOffset = 10.0f;

    float timer;
    float actualDistance;
    bool onRange;

    // PROGRESION
    GameObject spawnedEnemy;
    float newThrowerSpeed = 2.0f; // default lvl 1: 2
    float newMeleeSpeed = 2.0f;
    float newMeleeHealth = 100.0f;
    float newThrowerHealth = 80.0f;
    float newMeleeDamage = 50.0f;
    float newThrowerDamage = 30.0f;
    float newMCoins = 5.0f;
    float newTCoins = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _tr = transform;
        
        audioSource = GetComponent<AudioSource>();

        player = GameManager.Instance.GetPlayer();
        timer = 0;
        onRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        //Debug.Log(startTime);

        if (!GameManager.Instance.getMaxEnemies() && startTime <= 0) 
        {
            actualDistance = (transform.position - player.transform.position).magnitude;

            if (actualDistance > spawnDistance) onRange = false;
            else onRange = true;
            
            if (Time.time >= timer && onRange)
            {
                // si esta a cierta distancia cabe la posibilidad de ser thrower
                if (actualDistance > cacaThrowerDistance)
                {
                    // random
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        spawnYOffset = throwerEnemy.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.5f;
                        spawnPosition = _tr.position + _tr.up * spawnYOffset;
                        spawnedEnemy = Instantiate(throwerEnemy, spawnPosition, throwerEnemy.transform.rotation);
                    }
                    else
                    {
                        // melees
                        i = Random.Range(0, 2);
                        if (i == 0)
                        {
                            spawnYOffset = meleeEnemy.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.5f;
                            spawnPosition = _tr.position + _tr.up * spawnYOffset;
                            spawnedEnemy = Instantiate(meleeEnemy, transform.position, meleeEnemy.transform.rotation);
                        }
                        else
                        {
                            spawnYOffset = meleeEnemy2.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.5f;
                            spawnPosition = _tr.position + _tr.up * spawnYOffset;
                            spawnedEnemy = Instantiate(meleeEnemy2, transform.position, meleeEnemy2.transform.rotation);
                        }
                    }
                }
                else
                {
                    int j = Random.Range(0, 2);
                    if (j == 0)
                    {
                        spawnYOffset = meleeEnemy.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.5f;
                        spawnPosition = _tr.position + _tr.up * spawnYOffset;
                        spawnedEnemy = Instantiate(meleeEnemy, transform.position, meleeEnemy.transform.rotation);
                    }
                    else
                    {
                        spawnYOffset = meleeEnemy2.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.5f;
                        spawnPosition = _tr.position + _tr.up * spawnYOffset;
                        spawnedEnemy = Instantiate(meleeEnemy2, transform.position, meleeEnemy2.transform.rotation);
                    }
                }

                // setteamos enemigo segun level
                setEnemy(spawnedEnemy);

                GameManager.Instance.registerEnemy(spawnedEnemy);

                if (sonidosSpawn.Length > 0) // Verificar que haya sonidos asignados
                {
                    int i = Random.Range(0, sonidosSpawn.Length); // Elegir un sonido aleatorio
                    audioSource.PlayOneShot(sonidosSpawn[i], 0.5f);
                }

                timer = Time.time + spawnTime;
            }
        }
    }

    public void Upgrade(float meleeSp, float throwerSp, float meleeHP, float throwerHP,
        float meleeDmg, float throwerDmg, float meleeCoins, float throwerCoins)
    {
        newMeleeSpeed = meleeSp;
        newThrowerSpeed = throwerSp;
        newThrowerHealth = throwerHP;
        newMeleeHealth = meleeHP;
        newMeleeDamage = meleeDmg;
        newThrowerDamage = throwerDmg;
        newMCoins = meleeCoins;
        newTCoins = throwerCoins;

    }

    private void setEnemy(GameObject o)
    {
        if(o.GetComponent<CacaThrower>() != null)
        {
            o.GetComponent<AIMovement>().SetSpeed(newThrowerSpeed);
            o.GetComponent<Enemy>().SetHealth(newThrowerHealth);
            o.GetComponent<Enemy>()._damage = newThrowerDamage;
        }
        else
        {
            o.GetComponent<AIMovement>().SetSpeed(newMeleeSpeed);
            o.GetComponent<Enemy>().SetHealth(newMeleeHealth);
            o.GetComponent<Enemy>()._damage = newMeleeDamage;
        }
    }
}
