using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;

    [SerializeField] private float bulletSpeed = 5f;
    public int gunLevel = 1;

    [SerializeField] private float timeBetweenShots = 0.3f;

    [SerializeField] AudioClip soplidoSound;
    [SerializeField] AudioClip ayayay;

    private AudioSource audioSource;

    private int bounces = 0;  // Cuántos rebotes tendrá la bala

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

        // Reproduce sonido de disparo
        if (GameManager.Instance.GetBulletsLvl() >= 3)
        {
            if (!audioSource.isPlaying || audioSource.clip != ayayay)
            {
                audioSource.clip = ayayay;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.PlayOneShot(soplidoSound);
        }

        // Calculamos la dirección en la que se dispara, solo usando la cámara
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;  // Evita que se dispare hacia arriba/abajo
        cameraForward.Normalize();

        // Dirección de disparo
        Vector3 shootDirection;

        if (a)
        {
            shootDirection = cameraForward * (bulletSpeed + 3);
        }
        else
        {
            shootDirection = cameraForward * bulletSpeed;
        }

        // Instancia la pompa y la dispara
        if (gunLevel == 4)
        {
            var bubble = Instantiate(bulletPrefab2, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // Movemos la pompa en la dirección deseada
            StartCoroutine(MoveBubble(bubble.transform, shootDirection));
        }
        else
        {
            for (int i = 0; i < gunLevel; i++)
            {
                var bubble = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                // Movemos la pompa en la dirección deseada
                StartCoroutine(MoveBubble(bubble.transform, shootDirection));

                // Espera entre disparos
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    private IEnumerator MoveBubble(Transform bubbleTransform, Vector3 direction)
    {
        // Continuar moviendo la pompa mientras no se haya destruido
        while (bubbleTransform != null)
        {
            bubbleTransform.position += direction * Time.deltaTime;
            yield return null;
        }
    }

    public void MakeBouncyBubbles(int nbounces)
    {
        bounces = nbounces;  // Establece cuántos rebotes tiene la bala
    }

    private IEnumerator ReloadWeapon()
    {
        isReloading = true;
        GameManager.Instance.GetAnimationManager().rechargeAnim(true);

        bool wasMousePressed = Input.GetMouseButton(0);

        yield return new WaitForSeconds(5f);

        GameManager.Instance.GetAnimationManager().rechargeAnim(false);

        currentAmmo = gunLevel == 4
            ? GameManager.Instance.getARAmmo()
            : GameManager.Instance.getGunAmmo();

        if (wasMousePressed && Input.GetMouseButton(0))
        {
            yield return new WaitForSeconds(0.2f);
            GameManager.Instance.GetAnimationManager().attackAnim(true);
        }

        isReloading = false;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
