using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    [SerializeField] GameObject meleeEnemy;
    GameObject spawnedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemy = Instantiate(meleeEnemy, transform.position, meleeEnemy.transform.rotation);
        StartCoroutine(ScaleUpAndFall(spawnedEnemy));
        
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
        enabled = false;
    }
}
