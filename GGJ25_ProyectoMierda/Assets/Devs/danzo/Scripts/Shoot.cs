using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    [SerializeField] AudioClip soplidoSound;
    private AudioSource audioSource;

    private int bounces = 0;
    public int currentAmmo = 10;
    private bool isReloading = false;


    public void increaseGunLevel()
    {
        gunLevel++;
    }

    public void shootWeapon()
    {
        //Debug.Log("Municion:" + currentAmmo); 
        if (currentAmmo > 0)
        {
            StartCoroutine(ShootWithDelay());

        }
        else
        {
            if (!isReloading)
            {
                GameManager.Instance.GetAnimationManager().attackAnim(false);
                StartCoroutine(ReloadWeapon());
               

            }

        }



    }

    private IEnumerator ShootWithDelay()
    {
        currentAmmo--;
        audioSource.PlayOneShot(soplidoSound);

      
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; 

        cameraForward.Normalize();

        Vector3 playerVelocity = GameManager.Instance.GetPlayer().GetComponent<Rigidbody>().velocity;

        Vector3 shootDirection = cameraForward * bulletSpeed + Vector3.Project(playerVelocity, cameraForward);

        if (gunLevel == 4)
        {
            if (bounces == 0)
            {
                //Debug.Log("just normal bubble...");

                // Instancia la bala
                var bullet = Instantiate(bulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = shootDirection;
            }  
            else
            {

                //Debug.Log("MAKING BOUNCY BUBBLE");

                // Instancia la bala
                var bullet = Instantiate(bouncyBulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = shootDirection;
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
                    //Debug.Log("just normal bubble...");

                    // Instancia la bala
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = shootDirection;
                }
                else
                {

                   // Debug.Log("MAKING BOUNCY BUBBLE");

                    // Instancia la bala
                    var bullet = Instantiate(bouncyBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = shootDirection;
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

    private IEnumerator ReloadWeapon()
    {
        Debug.Log("holaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        isReloading = true;
        GameManager.Instance.GetAnimationManager().rechargeAnim(true);

        bool wasMousePressed = Input.GetMouseButton(0);

        yield return new WaitForSeconds(5f);

        GameManager.Instance.GetAnimationManager().rechargeAnim(false);

       


        if (gunLevel == 4)
            currentAmmo = GameManager.Instance.getARAmmo();
        else
            currentAmmo = GameManager.Instance.getGunAmmo();

        if (wasMousePressed && Input.GetMouseButton(0))
        {
           
            yield return new WaitForSeconds(0.2f); 
            GameManager.Instance.GetAnimationManager().attackAnim(true);
        }

        isReloading = false;
    

       
    }

    // Start is called before the first frame update
    void Start()
    {
        bounces = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
