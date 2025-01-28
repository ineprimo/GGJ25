using System.Collections;
using UnityEngine;

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
    private int bounces = 0;
    private bool canShoot = true;
    public int currentAmmo = 10;
    public bool isReloading = false;

    public void shootWeapon(bool a)
    {
        if (isReloading || !canShoot) return;

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
        canShoot = false;
        PlayShootSound();
        Vector3 shootDirection = GetShootDirection(a);

        for (int i = 0; i < gunLevel; i++)
        {
            GameObject bubble = InstantiateBubble();
            StartCoroutine(MoveBubble(bubble.transform, shootDirection));
            yield return new WaitForSeconds(timeBetweenShots);
        }

        currentAmmo--;
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

    public void MakeBouncyBubbles(int nbounces) => bounces = nbounces;

    private IEnumerator ReloadWeapon()
    {
        isReloading = true;
        GameManager.Instance.GetAnimationManager().rechargeAnim(true);

        yield return new WaitForSeconds(2.5f);

        GameManager.Instance.GetAnimationManager().rechargeAnim(false);
        currentAmmo = gunLevel == 4
            ? GameManager.Instance.getARAmmo()
            : GameManager.Instance.getGunAmmo();

        isReloading = false;
    }

    void Start() => audioSource = GetComponent<AudioSource>();
}
