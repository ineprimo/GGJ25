using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance; // Para acceder fácilmente desde otros scripts

    [SerializeField] private float duration = 0.1f; // Duración del temblor
    [SerializeField] private float magnitude = 0.1f; // Intensidad del temblor

    private Vector3 originalPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
