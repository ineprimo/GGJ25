using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaMovement : MonoBehaviour
{
    GameObject target;
    [SerializeField] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player"); // sorry 
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        Vector3 v3 = target.transform.position - transform.position;
        v3.y = -90.0f;
        transform.rotation = Quaternion.LookRotation(v3);
    }
}
