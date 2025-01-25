using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnTriggerTestBouncyBubble : MonoBehaviour
{
    private float cd = 5;
    private float currentTime = 0;

    private bool isOnCD = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerMovement>() != null && !isOnCD)
        {
            GameManager.Instance.UpgradeDamage();
            isOnCD = true;  
        }
    }

    private void Update()
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
}
