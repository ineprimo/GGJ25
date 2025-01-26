using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shoot : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;


    [SerializeField] private float bulletSpeed = 5;
    public int gunLevel = 1;

    [SerializeField] private float timeBetweenShots = 0.3f;

    [SerializeField] AudioClip soplidoSound;
    [SerializeField] AudioClip ayayay;

    private AudioSource audioSource;

    private int bounces = 0;
    public int currentAmmo = 10;
    private bool isReloading = false;


    public void increaseGunLevel()
    {
        gunLevel++;
    }

    public void shootWeapon(bool a)
    {
        if (currentAmmo > 0)
        {
            StartCoroutine(ShootWithDelay(a));

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

    private IEnumerator ShootWithDelay(bool a)
    {
        currentAmmo--;
        

        if (GameManager.Instance.GetBulletsLvl()>=3 )
        {

            // Reproduce "ayayay" solo si no está ya sonando
            if (!audioSource.isPlaying || audioSource.clip != ayayay)
            {
                audioSource.clip = ayayay;
                audioSource.Play();
            }
        }
        else
            audioSource.PlayOneShot(soplidoSound);

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;

        cameraForward.Normalize();

        Vector3 playerVelocity = GameManager.Instance.GetPlayer().GetComponent<Rigidbody>().velocity;

        Vector3 shootDirection;

        if (a)
        {
            shootDirection = cameraForward * (bulletSpeed + 3);
        }
        else
            shootDirection = cameraForward * bulletSpeed + Vector3.Project(playerVelocity, cameraForward);

        if (gunLevel == 4)
        {
            // Instancia la bala
            var bullet = Instantiate(bulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shootDirection;
        }
        else
        {
            for (int i = 0; i < gunLevel; i++)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = shootDirection;

                // Espera el tiempo entre disparos antes de instanciar la siguiente bala
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    public void MakeBouncyBubbles(int nbounces)
    {
        bounces = nbounces;
    }

    private IEnumerator ReloadWeapon()
    {
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
