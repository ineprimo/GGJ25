using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    [SerializeField] GameObject meleeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(meleeEnemy, transform.position, meleeEnemy.transform.rotation);
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
