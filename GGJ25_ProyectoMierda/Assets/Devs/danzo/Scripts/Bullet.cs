using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1.0f;
    [SerializeField] private float deceleration = 4.5f;

    [SerializeField] private float shakeDuration = 2000.0f; 
    [SerializeField] private float shakeAmount = 100.0f;  

    private Rigidbody rb;
    private bool isShaking = false;
    private Vector3 originalPosition;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
       // originalPosition = transform.position;
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
            //if (!isShaking)
            //{
            //    Debug.Log("sfnsfn");
            //    isShaking = true;
            //    ShakeAndDestroy();
            //}
        }
   
   
    }

    private void ShakeAndDestroy()
    {
        
        float elapsedTime = 0;

        
        while (elapsedTime < lifeTime)
        {
            float xOffset = Mathf.Sin(elapsedTime * Mathf.PI * 2 / shakeDuration) * 1000;  
            float yOffset = Mathf.Cos(elapsedTime * Mathf.PI * 2 / shakeDuration) * 1000;  

            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);
            Debug.Log(elapsedTime);
            elapsedTime += Time.deltaTime;
           
        }

        transform.position = originalPosition;
 
    }

}
