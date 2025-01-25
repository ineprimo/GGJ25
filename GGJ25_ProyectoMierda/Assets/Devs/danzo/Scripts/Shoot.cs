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
    [SerializeField] private GameObject ammo;
    [SerializeField] private float bulletSpeed = 5;
    public int gunLevel = 1;

    [SerializeField] private float timeBetweenShots = 0.3f;

    private int bounces = 0;
    public int currentAmmo = 10;
    private int maxAmmo;
   


    public void shootWeapon()
    {
        Debug.Log(currentAmmo);
        if (currentAmmo > 0)
        {
            Debug.Log("entra");
           
        }
        StartCoroutine(ShootWithDelay());

    }

    private IEnumerator ShootWithDelay()
    {
        currentAmmo--;

        if (gunLevel == 4)
        {
            if (bounces == 0)
            {
                Debug.Log("just normal bubble...");

                // Instancia la bala
                var bullet = Instantiate(bulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            }  
            else
            {

                Debug.Log("MAKING BOUNCY BUBBLE");

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

                //Debug.Log("bounces " + bounces);
                // Instancia la bala
                if (bounces == 0)
                {
                    Debug.Log("just normal bubble...");

                    // Instancia la bala
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                }
                else
                {

                    Debug.Log("MAKING BOUNCY BUBBLE");

                    // Instancia la bala
                    var bullet = Instantiate(bouncyBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                    bullet.GetComponent<BounceBubble>().setBounces(bounces);
                }


                // Espera el tiempo entre disparos antes de instanciar la siguiente bala
                yield return new WaitForSeconds(timeBetweenShots);
            }
          

            
        }
       
    }

    public void MakeBouncyBubbles(int nbounces)
    {
        Debug.Log("MAKING BOUNCY BUBBLES");
        bounces = nbounces;
        Debug.Log("bounces " + bounces);
    }
    // Start is called before the first frame update
    void Start()
    {
        bounces = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        maxAmmo = 10 * gunLevel;
        Debug.Log(gunLevel);  
    }
}
