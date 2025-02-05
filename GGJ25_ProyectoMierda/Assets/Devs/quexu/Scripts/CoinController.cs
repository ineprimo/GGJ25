using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 75f; // Velocidad de rotación
    [SerializeField] private int value = 1; // Valor de la moneda
    [SerializeField] private AudioClip pickupSound; // Sonido al recoger la moneda
    [SerializeField] private float moveSpeed = 10f; // Velocidad de movimiento hacia el jugador
    [SerializeField] private float spawnScaleDuration = 0.5f; // Duración de la animación de aparición

    private bool isCollected = false; // Bandera para evitar que la moneda se recoja múltiples veces

    private void Start()
    {
        // Llamar a la corutina para la animación de aparición
        StartCoroutine(ScaleUpAnimation());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player" y si la moneda no ha sido recogida ya
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true; // Marcar la moneda como recogida
            gameObject.GetComponent<BoxCollider>().enabled = false;
            // Inicia la corrutina para mover la moneda hacia el jugador
            StartCoroutine(MoveCoinToPlayer(other.gameObject));

            // Añadir las monedas al GameManager
            GameManager.Instance.addCoins(value);

            // Reproducir sonido en el jugador
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound, 0.5f);
            }
        }
    }

    // IEnumerator para mover la moneda hacia el jugador antes de destruirla
    public IEnumerator MoveCoinToPlayer(GameObject player)
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
        StartCoroutine(ScaleDownAnimation()); // Comienza la animación de disminución

        // Destruir la moneda después de moverla
        Destroy(gameObject);
    }

    private IEnumerator ScaleDownAnimation()
    {
        Vector3 initialScale = transform.localScale; // Escala actual de la moneda
        Vector3 targetScale = Vector3.zero; // Escala final (0, 0, 0)

        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 0.5f;

            // Interpolación lineal entre la escala inicial y la final
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que la escala final sea exactamente la deseada
        transform.localScale = targetScale;
    }

    // Corutina para escalar la moneda desde 0 hasta su tamaño normal
    private IEnumerator ScaleUpAnimation()
    {
        Vector3 initialScale = Vector3.zero; // Escala inicial (0, 0, 0)
        Vector3 targetScale = new Vector3(0.6877028f, 0.6877028f, 0.6877028f);

        float elapsedTime = 0f;

        while (elapsedTime < spawnScaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / spawnScaleDuration;

            // Interpolación lineal entre la escala inicial y la final
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que la escala final sea exactamente la deseada
        transform.localScale = targetScale;
    }

    // Update es llamado una vez por frame
    void Update()
    {
        // Rotar continuamente el objeto en el eje Y
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
