using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bouncyBulletPrefab;
    [SerializeField] private float bulletSpeed = 5;
    [SerializeField] private int gunLevel = 1;

    [SerializeField] private float timeBetweenShots = 0.3f;

    private int bounces = 0;


    public void shootWeapon()
    {
        StartCoroutine(ShootWithDelay());
    }

    private IEnumerator ShootWithDelay()
    {
        for (int i = 0; i < gunLevel; i++)
        {
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
        
    }
}
