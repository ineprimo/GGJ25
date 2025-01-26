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

    private float currentTime = 0;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
       // Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
            GetComponent<PlaySoundWithVariation>().Reproduce();
        Destroy(gameObject);
    }


    private void Update()
    {  

        if (rb.velocity.magnitude > 0)
        {
            rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;

        }
        currentTime += Time.deltaTime;
        if(currentTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }


}
