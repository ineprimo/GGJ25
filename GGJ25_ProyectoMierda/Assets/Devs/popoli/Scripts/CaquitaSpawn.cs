using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaSpawn : MonoBehaviour
{
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
    float newMeleeSpeed = 2.0f; // default lvl 1: 2

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();
        timer = 0;
        onRange = false;

        spawnPosition = new Vector3(transform.position.x, transform.position.y + spawnYOffset, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        //Debug.Log(startTime);

        if (!GameManager.Instance.getMaxEnemies() && startTime <= 0) 
        {
            actualDistance = (transform.position - player.transform.position).magnitude;

            if (actualDistance < spawnDistance) onRange = false;
            else onRange = true;

            //Debug.Log(onRange);
            
            if (Time.time >= timer && onRange)
            {
                // si esta a cierta distancia cabe la posibilidad de ser thrower
                if (actualDistance > cacaThrowerDistance)
                {
                    // random
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        spawnedEnemy = Instantiate(throwerEnemy, spawnPosition, throwerEnemy.transform.rotation);
                    }
                    else
                    {
                        int i1 = Random.Range(0, 2);
                        if (i1 == 0)
                        {
                            spawnedEnemy = Instantiate(meleeEnemy, transform.position, throwerEnemy.transform.rotation);
                        }
                        else
                        {
                            spawnedEnemy = Instantiate(meleeEnemy2, transform.position, throwerEnemy.transform.rotation);
                        }
                    }
                }
                else
                {
                    int i1 = Random.Range(0, 2);
                    if (i1 == 0)
                    {
                        spawnedEnemy = Instantiate(meleeEnemy, transform.position, throwerEnemy.transform.rotation);
                    }
                    else
                    {
                        spawnedEnemy = Instantiate(meleeEnemy2, transform.position, throwerEnemy.transform.rotation);
                    }
                }

                // setteamos enemigo segun level
                setEnemy(spawnedEnemy);

                timer = Time.time + spawnTime;
            }
        }
    }

    public void Upgrade(float meleeSp, float throwerSp)
    {
        newMeleeSpeed = meleeSp;
        newThrowerSpeed = throwerSp;
        
    }

    public void setEnemy(GameObject o)
    {
        if(o.GetComponent<CacaThrower>() != null)
        {
            o.GetComponent<AIMovement>().SetSpeed(newThrowerSpeed);
        }
        else
        {
            o.GetComponent<AIMovement>().SetSpeed(newMeleeSpeed);
        }

        GameManager.Instance.registerEnemy(o);
    }
}
