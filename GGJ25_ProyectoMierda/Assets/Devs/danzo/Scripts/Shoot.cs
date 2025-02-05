using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;

    [SerializeField] private float bulletSpeed = 5f;
    public int gunLevel = 1;

    [SerializeField] private float delayBeforeShot = 0.1f; // Pequeño retraso antes del primer disparo
    [SerializeField] public float timeBetweenShots = 0.3f;

    [SerializeField] private Light[] effectLights; // Array de luces para cambiar de color
    [SerializeField] private Color[] lightColors; // Colores para cambiar durante el disparo
    private int currentColorIndex = 0;

    [SerializeField] AudioClip soplidoSound;
    [SerializeField] AudioClip ayayay;
    public bool isShooting = false;

    private AudioSource audioSource;
    private bool canShoot = true;

    public void shootWeapon(bool a)
    {
        if (!canShoot) return;
        isShooting = true;
        StartCoroutine(ShootWithDelay(a));
        if (gunLevel == 4)
        {
            StartCoroutine(ChangeLightEffect());
        }
    }

    private IEnumerator ShootWithDelay(bool a)
    {
        canShoot = false;
        PlayShootSound();

        Vector3 shootDirection = GetShootDirection(a);
        GameObject bubble = InstantiateBubble();
        StartCoroutine(MoveBubble(bubble.transform, shootDirection));

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private Vector3 GetShootDirection(bool a)
    {
        Vector3 shootDirection = Camera.main.transform.forward;
        return shootDirection * (a ? (bulletSpeed + 3) : bulletSpeed);
    }

    private GameObject InstantiateBubble()
    {
        GameObject bubblePrefab = gunLevel == 4 ? bulletPrefab2 : bulletPrefab;
        return Instantiate(bubblePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    private void PlayShootSound()
    {
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
    }

    private IEnumerator MoveBubble(Transform bubbleTransform, Vector3 direction)
    {
        while (bubbleTransform != null)
        {
            bubbleTransform.position += direction * Time.deltaTime;
            yield return null;
        }
    }

    public void increaseGunLevel() => gunLevel++;

    public void IncreaseFireRate(float amount)
    {
        timeBetweenShots = Mathf.Max(0.05f, timeBetweenShots - amount); // No baja de 0.05s
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetLightsIntensity(0); // Apagar luces al inicio
    }

    private IEnumerator ChangeLightEffect()
    {
        while (isShooting && gunLevel == 4)
        {
            SetLightsIntensity(.005f); // Ajusta la intensidad a un valor visible
            ChangeLightsColor();
            yield return new WaitForSeconds(0.2f); // Cambia de color cada 0.2s
        }
        SetLightsIntensity(0); // Cuando deja de disparar, apaga las luces
    }

    private void ChangeLightsColor()
    {
        if (lightColors.Length == 0 || effectLights.Length == 0) return;

        currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
        foreach (Light light in effectLights)
        {
            light.color = lightColors[currentColorIndex];
        }
    }

    private void SetLightsIntensity(float intensity)
    {
        foreach (Light light in effectLights)
        {
            light.intensity = intensity;
        }
    }

    public void StopShooting()
    {
        isShooting = false;
    }
}