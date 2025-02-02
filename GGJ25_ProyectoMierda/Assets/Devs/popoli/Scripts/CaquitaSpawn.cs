using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaSpawn : MonoBehaviour
{
    public AudioClip[] sonidosSpawn;
    private AudioSource audioSource;
    private Transform _tr;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] GameObject meleeEnemy;
    [SerializeField] GameObject meleeEnemy2;
    [SerializeField] GameObject throwerEnemy;

    private GameObject player;
    [SerializeField] public float spawnTime = 3.0f;
    [SerializeField] float spawnDistance = 2.0f;
    [SerializeField] float cacaThrowerDistance = 7.0f;
    [SerializeField] float startTime = 5.0f;
    
    Vector3 spawnPosition;
    [SerializeField] float spawnYOffset = 10.0f;

    float newSpawnTime;
    float actualDistance;
    bool onRange;

    GameObject spawnedEnemy;

    float newThrowerSpeed = 2.0f;
    float newMeleeSpeed = 2.0f;
    float newMeleeHealth = 100.0f;
    float newThrowerHealth = 80.0f;
    float newMeleeDamage = 200.0f;
    float newThrowerDamage = 200.0f;
    float newMCoins = 5.0f;
    float newTCoins = 5.0f;


    void Start()
    {
        _tr = transform;
        audioSource = GetComponent<AudioSource>();
        player = GameManager.Instance.GetPlayer();
        onRange = false;
        newSpawnTime = 30;

        spawnPosition = _spawnPoint.position + _tr.up * spawnYOffset;
    }

    void Update()
    {
        startTime -= Time.deltaTime;

        if (!GameManager.Instance.getMaxEnemies() && startTime <= 0) 
        {
            actualDistance = (transform.position - player.transform.position).magnitude;

            if (actualDistance > spawnDistance) onRange = false;
            else
            {
                onRange = true;
                spawnTime -= Time.deltaTime;
            }

            if (spawnTime <= 0 && onRange)
            {
                if (actualDistance > cacaThrowerDistance)
                {
                    int i = Random.Range(0, 2);
                    if (i == 0)
                        spawnedEnemy = Instantiate(throwerEnemy, spawnPosition, throwerEnemy.transform.rotation);
                    else
                        spawnedEnemy = Instantiate(meleeEnemy, spawnPosition, meleeEnemy.transform.rotation);
                }
                else
                {
                    int j = Random.Range(0, 2);
                    if (j == 0)
                        spawnedEnemy = Instantiate(meleeEnemy, spawnPosition, meleeEnemy.transform.rotation);
                    else
                        spawnedEnemy = Instantiate(meleeEnemy2, spawnPosition, meleeEnemy2.transform.rotation);
                }

                setEnemy(spawnedEnemy);
                GameManager.Instance.registerEnemy(spawnedEnemy);

                if (sonidosSpawn.Length > 0)
                {
                    int i = Random.Range(0, sonidosSpawn.Length);
                    audioSource.PlayOneShot(sonidosSpawn[i], 0.5f);
                }

                StartCoroutine(ScaleUpAndFall(spawnedEnemy));

                spawnTime = newSpawnTime;
            }
        }
    }

    private IEnumerator ScaleUpAndFall(GameObject enemy)
    {
        Vector3 defaultScale = enemy.transform.localScale;
        enemy.transform.localScale = Vector3.zero;

        AIMovement aiMovement = enemy.GetComponent<AIMovement>();
        if (aiMovement != null)
            aiMovement.enabled = false;

        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        if (rb == null)
            rb = enemy.AddComponent<Rigidbody>();

        rb.useGravity = false;

        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (enemy != null)
            {
                enemy.transform.localScale = Vector3.Lerp(Vector3.zero, defaultScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            else
                break;
            
        }

        enemy.transform.localScale = defaultScale;

        if (aiMovement != null)
            aiMovement.enabled = true;

       
        rb.useGravity = true;
    }

    public void Upgrade(float meleeSp, float throwerSp, float meleeHP, float throwerHP,
        float meleeDmg, float throwerDmg, float meleeCoins, float throwerCoins, float spTime)
    {
        newMeleeSpeed = meleeSp;
        newThrowerSpeed = throwerSp;
        newThrowerHealth = throwerHP;
        newMeleeHealth = meleeHP;
        newMeleeDamage = meleeDmg;
        newThrowerDamage = throwerDmg;
        newMCoins = meleeCoins;
        newTCoins = throwerCoins;
        newSpawnTime = spTime;
    }

    private void setEnemy(GameObject o)
    {
        if (o.GetComponent<CacaThrower>() != null)
        {
            o.GetComponent<AIMovement>().SetSpeed(newThrowerSpeed);
            o.GetComponent<Enemy>().SetHealth(newThrowerHealth);
            o.GetComponent<Enemy>()._damage = newThrowerDamage / 100;
        }
        else
        {
            o.GetComponent<AIMovement>().SetSpeed(newMeleeSpeed);
            o.GetComponent<Enemy>().SetHealth(newMeleeHealth);
            o.GetComponent<Enemy>()._damage = newMeleeDamage / 100;
        }
    }
}
