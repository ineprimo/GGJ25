using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaSpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] float spawnTime = 3.0f; //seconds
    [SerializeField] float distance = 2.0f;

    float timer;
    float actualDistance;
    bool onRange;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        onRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        actualDistance = (transform.position - player.transform.position).magnitude;

        if (actualDistance < distance) onRange = false;
        else onRange = true;

        //Debug.Log(onRange);

        if (Time.time >= timer && onRange)
        {
            //Debug.Log("instancia caquita");
            Instantiate(enemy, transform.position, enemy.transform.rotation);
            timer = Time.time + spawnTime;
        }
    }
}
