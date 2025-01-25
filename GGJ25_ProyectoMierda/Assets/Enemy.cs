using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

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
        
        if(_health <= 0)
            Destroy(gameObject);
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
