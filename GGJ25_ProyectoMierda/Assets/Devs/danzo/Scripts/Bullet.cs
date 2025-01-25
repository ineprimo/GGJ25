using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float _damage = 10.0f;
    [SerializeField] private float lifeTime = 1.0f;
    [SerializeField] private float deceleration = 4.5f;

    private Rigidbody rb;
    public float Damage { get { return _damage; } }

    private bool isOnCD = false;
    private float currentTime = 0;
    private float cd = 3;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
       // Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("checking collision");
       // Destroy(collision.gameObject);
       if(GetComponent<BounceBubble>() != null && GetComponent<BounceBubble>().getBounces() > 0 && !isOnCD)
       {
            Debug.Log("is bouncy");
            //  && GetComponent<BounceBubble>().getBounces() > 0
            // le resta un rebote
            GetComponent<BounceBubble>().setBounces(GetComponent<BounceBubble>().getBounces() - 1);

            changeDirection();

            isOnCD = true;

       }
        else
        {
            GetComponent<PlaySoundWithVariation>().Reproduce();
            Destroy(gameObject);
        }
    }

    private void changeDirection()
    {

        Debug.Log("REBOTA YA COJONEEEEEEEEEEEEEEEEEEEEEES");
        //Vector3 dir = GetComponent<Rigidbody>().velocity;

        //Vector3 left = Vector3.Cross(dir, Vector3.up).normalized;

        //Vector3 right = -left;
        ////Vector3 right = Vector3.Cross(Vector3.up, dir).normalized;
        ////Vector3 right = Vector3.Cross(-dir, Vector3.up).normalized;
        ////Vector3 right = Vector3.Cross(dir, -Vector3.up).normalized;

        ////GetComponent<Rigidbody>().velocity = left; // esto no es lmaooooo
        //GetComponent<Rigidbody>().AddForce(left); // esto no es lmaooooo


        var opposite = -GetComponent<Rigidbody>().velocity * 20;

        // mover la z

        Debug.Log("initial " + GetComponent<Rigidbody>().velocity + " opposite " + opposite);

        GetComponent<Rigidbody>().AddForce(opposite * Time.deltaTime);

    }

    private void bounceCD()
    {
        if (isOnCD)
        {
            // cooldown
            if (cd <= currentTime)
            {
                isOnCD = false;
                currentTime = 0;
            }
            else
                currentTime += Time.deltaTime;
        }
    }

    private void Update()
    {  

        if (rb.velocity.magnitude > 0)
        {
          //  Debug.Log("sfnsfn33333");
            rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;

        }
        currentTime += Time.deltaTime;
        if(currentTime >= lifeTime)
        {
            GetComponent<PlaySoundWithVariation>().Reproduce();
            Destroy(gameObject);
        }
   
        bounceCD();
    }


}
