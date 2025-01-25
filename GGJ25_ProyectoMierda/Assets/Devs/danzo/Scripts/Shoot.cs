using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private GameObject bouncyBulletPrefab;
    [SerializeField] private GameObject bouncyBulletPrefab2;
    [SerializeField] private float bulletSpeed = 5;
    public int gunLevel = 1;

    [SerializeField] private float timeBetweenShots = 0.3f;

    private int bounces = 0;


    public void shootWeapon()
    {
        StartCoroutine(ShootWithDelay());
    }

    private IEnumerator ShootWithDelay()
    {
        if(gunLevel == 4)
        {
            if (bounces == 0)
            {
                // Instancia la bala
                var bullet = Instantiate(bulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            }  
            else
            {
                // Instancia la bala
                var bullet = Instantiate(bouncyBulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                bullet.GetComponent<BounceBubble>().setBounces(bounces);
            }

        }
        else
        {
            for (int i = 0; i < gunLevel; i++)
            {
                // Instancia la bala
                if (bounces == 0)
                {
                    // Instancia la bala
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                }
                else
                {
                    // Instancia la bala
                    var bullet = Instantiate(bouncyBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                    bullet.GetComponent<BounceBubble>().setBounces(bounces);
                }
            }
          

                // Espera el tiempo entre disparos antes de instanciar la siguiente bala
                yield return new WaitForSeconds(timeBetweenShots);
            
        }
       
    }

    public void MakeBouncyBubbles(int nbounces)
    {
        bounces = nbounces;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gunLevel);  
    }
}
