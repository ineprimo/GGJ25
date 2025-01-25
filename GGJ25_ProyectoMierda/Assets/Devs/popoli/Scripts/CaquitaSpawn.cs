using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaSpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private GameObject player;
    [SerializeField] float spawnTime = 3.0f; //seconds
    [SerializeField] float spawnDistance = 2.0f; // distancia para spawnear
    [SerializeField] float cacaThrowerDistance = 7.0f; // distancia para spawnear un caca thrower

    [SerializeField] float startTime = 5.0f; // tiempo para que empiece a spawnear

    float timer;
    float actualDistance;
    bool onRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();
        timer = 0;
        onRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        Debug.Log(startTime);

        if (!GameManager.Instance.getMaxEnemies() && startTime <= 0) 
        {
            actualDistance = (transform.position - player.transform.position).magnitude;

            if (actualDistance < spawnDistance) onRange = false;
            else onRange = true;

            //Debug.Log(onRange);

            if (Time.time >= timer && onRange)
            {
                //Debug.Log("instancia caquita");
                GameObject clone = Instantiate(enemy, transform.position, enemy.transform.rotation);
                GameManager.Instance.registerEnemy();

                // si esta a cierta distancia le doy el caca thrower
                if (actualDistance > cacaThrowerDistance)
                {
                    // random
                    int i = Random.Range(0, 2);
                    if (i == 0) clone.AddComponent<CacaThrower>();
                }

                timer = Time.time + spawnTime;
            }
        }
    }
}
