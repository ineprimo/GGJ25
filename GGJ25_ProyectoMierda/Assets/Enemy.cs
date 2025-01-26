using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _damage = 10.0f;
    [SerializeField] private float _health;
    [SerializeField] private GameObject coin;
    [SerializeField] private float threshold = 0.5f;

    private const int SCORE_MELEE = 29;
    private const int SCORE_DISTANCE = 39;

    public void SetHealth(float h)
    {
        _health = h;
    }

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
    
    // cuando la bala burbuja hittee al enemy 
    public void Hit(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            GetComponent<Animator>().SetTrigger("death");
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            StartCoroutine(Death());
        }
    }

    public void Bomba()
    {
        float f = UnityEngine.Random.Range(0f, 1f);
        if(f < threshold)
            Instantiate(coin, transform.position, transform.rotation);

        GameManager.Instance.deRegisterEnemy(gameObject);
        Destroy(gameObject);

        if(gameObject.GetComponent<CacaThrower>() != null)
            GameManager.Instance.increaseScore(SCORE_DISTANCE);
        else
            GameManager.Instance.increaseScore(SCORE_MELEE);
    }
    
    private IEnumerator Death()
    {
        transform.localScale *= 0.25f;
        
        yield return new WaitForSeconds(3);
        
        GetComponent<Animator>().SetTrigger("confetti");
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
