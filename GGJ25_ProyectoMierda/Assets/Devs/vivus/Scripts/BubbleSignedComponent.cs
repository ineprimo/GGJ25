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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().Hit(_bubbleSignedDamage);
            Destroy(gameObject);
        }
    }
}
