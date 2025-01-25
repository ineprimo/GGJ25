using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float _damage = 10.0f;
    [SerializeField] private float lifeTime = 1.0f;
    [SerializeField] private float deceleration = 4.5f;

    private Rigidbody rb;

    public float Damage { get; private set; }
    
    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Destroy(collision.gameObject);
       if(GetComponent<BounceBubble>() != null && GetComponent<BounceBubble>().getBounces() > 0)
       {
            // le resta un rebote
            GetComponent<BounceBubble>().setBounces(GetComponent<BounceBubble>().getBounces() - 1);

            changeDirection();



       }
       else
            Destroy(gameObject);
    }

    private void changeDirection()
    {

        Debug.Log("hola????");
        Vector3 dir = GetComponent<Rigidbody>().velocity;

        Vector3 left = Vector3.Cross(dir, Vector3.up).normalized;

        Vector3 right = -left;
        //Vector3 right = Vector3.Cross(Vector3.up, dir).normalized;
        //Vector3 right = Vector3.Cross(-dir, Vector3.up).normalized;
        //Vector3 right = Vector3.Cross(dir, -Vector3.up).normalized;

        //GetComponent<Rigidbody>().velocity = left; // esto no es lmaooooo
        GetComponent<Rigidbody>().AddForce(left); // esto no es lmaooooo

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
