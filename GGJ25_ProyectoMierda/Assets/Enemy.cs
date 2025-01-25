using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private GameObject coin;
    [SerializeField] private float threshold = 0.5f;

    public bool Frozen { get; private set; } = false;

    public void Freeze()
    {
        Frozen = true;
        GetComponent<CaquitaMovement>().enabled = false;
    }

    public void Unfreeze()
    {
        Frozen = false;
        GetComponent<CaquitaMovement>().enabled = true;
    }
    
    public void Hit(float damage)
    {
        _health -= damage;

        Debug.Log(_health);

        if(_health <= 0)
        {
            float f = UnityEngine.Random.Range(0f, 1f);
            if(f < threshold)
               Instantiate(coin, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            Hit(other.gameObject.GetComponent<Bullet>().Damage);
            Destroy(other.gameObject);
        }
    }
}
