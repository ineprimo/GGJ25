using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 75f; // Velocidad de rotación
    [SerializeField] private int value = 1; // Valor de la moneda
    [SerializeField] private AudioClip pickupSound; // Sonido al recoger la moneda
    [SerializeField] private float moveSpeed = 10f; // Velocidad de movimiento hacia el jugador

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            // Inicia la corrutina para mover la moneda hacia el jugador
            StartCoroutine(MoveCoinToPlayer(other.gameObject));

            // Añadir las monedas al GameManager
            GameManager.Instance.addCoins(value);

            // Reproducir sonido en el jugador
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
        }
    }

    // IEnumerator para mover la moneda hacia el jugador antes de destruirla
    private IEnumerator MoveCoinToPlayer(GameObject player)
    {
        Vector3 startPosition = transform.position; // Posición inicial de la moneda
        Vector3 targetPosition = player.transform.position; // Posición del jugador
        

        float journeyLength = Vector3.Distance(startPosition, targetPosition); // Distancia entre la moneda y el jugador
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) // Mientras no llegue lo suficientemente cerca
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed; // Distancia recorrida
            float fractionOfJourney = distanceCovered / journeyLength; // Porcentaje del recorrido completado

            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney); // Mover la moneda

            yield return null; // Esperar un frame
        }

        // Asegurarse de que la moneda termine en la posición del jugador
        transform.position = targetPosition;

        // Destruir la moneda después de moverla
        Destroy(gameObject);
    }

    // Update es llamado una vez por frame
    void Update()
    {
        // Rotar continuamente el objeto en el eje Z
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
