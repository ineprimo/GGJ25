using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5;
    [SerializeField] private int gunLevel = 1;

    [SerializeField] private float timeBetweenShots = 0.3f;


    public void shootWeapon()
    {
        StartCoroutine(ShootWithDelay());
    }

    private IEnumerator ShootWithDelay()
    {
        for (int i = 0; i < gunLevel; i++)
        {
            // Instancia la bala
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

            // Espera el tiempo entre disparos antes de instanciar la siguiente bala
            yield return new WaitForSeconds(timeBetweenShots);
        }
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
