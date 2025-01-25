using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterGachapon : MonoBehaviour
{
    [SerializeField] private bool canPull;
    [SerializeField] private bool gachaOnCooldown;
    [SerializeField] private GachaponBase _gacha;

    [SerializeField] private float currentGachaTime = 0;

    private void Start()
    {
        canPull = false;
        gachaOnCooldown = false;

        _gacha.PrepareGacha();
    }
    private void Update()
    {
        if(canPull)
        {

            if (Input.GetKey(KeyCode.E) && !gachaOnCooldown)
            {
                // esto va cuando se quiera hacer un pull en el gachapon
                Upgrade up = _gacha.pull();

                gachaOnCooldown = true;

                if (up == null)
                    Debug.Log("no upgrade avaliable");
                else
                    Debug.Log(up.getName());

                updateUpgrades(up.getName());
            }

            if (gachaOnCooldown)
            {

                // cooldown
                if (_gacha.getCD() <= currentGachaTime)
                {
                    gachaOnCooldown=false;
                    currentGachaTime = 0;
                }
                else
                    currentGachaTime += Time.deltaTime;
            }
        }
    }

    // BULLETS, LIFE, SPEED, DAMAGE
    private void updateUpgrades(string up)
    {
        switch (up)
        {
            case "BULLETS":
                GameManager.Instance.UpgradeBullets();
                break;
            case "LIFE":
                GameManager.Instance.UpgradeLife();
                break;
            case "SPEED":
                GameManager.Instance.UpgradeSpeed();
                break;
            case "DAMAGE":
                GameManager.Instance.UpgradeDamage();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // si es el player
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            // le permite tirar en el gachapon
            canPull = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // si es el player
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            // le permite tirar en el gachapon
            canPull = false;
        }
    }
}
