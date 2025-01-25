using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSignedComponent : MonoBehaviour
{
    [SerializeField] private float _bubbleSignedDamage = 0;

    public void SetDamage(float dm)
    {
        _bubbleSignedDamage = dm;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().Hit(_bubbleSignedDamage);
            Destroy(gameObject);
        }
    }
}
