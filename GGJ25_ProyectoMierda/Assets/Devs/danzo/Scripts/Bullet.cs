using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1.0f;
    [SerializeField] private float deceleration = 4.5f;

    private Rigidbody rb;


    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {  
        if (rb.velocity.magnitude > 0)
        {
          //  Debug.Log("sfnsfn33333");
            rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;

        }
   
   
    }


}
