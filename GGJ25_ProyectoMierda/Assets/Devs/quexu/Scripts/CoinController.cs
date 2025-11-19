using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 75f;
    [SerializeField] private int value = 1;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float spawnScaleDuration = 0.5f;

    private bool isCollected = false;

    private void Start()
    {
        StartCoroutine(ScaleUpAnimation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(MoveCoinToPlayer(other.gameObject));


            // 1. Sumar al GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.addCoins(value);
            }

            // 2. Llamar al nuevo Audio Manager para el sonido con pitch dinámico
            if (CoinAudioManager.Instance != null)
            {
                CoinAudioManager.Instance.PlayCoinSound();
            }
            else
            {
                Debug.LogWarning("Falta el CoinAudioManager en la escena");
            }
        }
    }


    public IEnumerator MoveCoinToPlayer(GameObject player)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = player.transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        transform.position = targetPosition;
        StartCoroutine(ScaleDownAnimation());
        Destroy(gameObject);
    }

    private IEnumerator ScaleDownAnimation()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 0.5f;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        transform.localScale = targetScale;
    }

    private IEnumerator ScaleUpAnimation()
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = new Vector3(0.6877028f, 0.6877028f, 0.6877028f);
        float elapsedTime = 0f;
        while (elapsedTime < spawnScaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / spawnScaleDuration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        transform.localScale = targetScale;
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}