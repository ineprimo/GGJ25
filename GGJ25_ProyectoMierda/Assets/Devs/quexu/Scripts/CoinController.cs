using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 75f; // Velocidad de rotación
    [SerializeField] private int value = 1; // Valor de la moneda
    [SerializeField] private AudioClip pickupSound; // Sonido al recoger la moneda

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            // Añadir las monedas al GameManager
            GameManager.Instance.addCoins(value);

            // Reproducir sonido en el jugador
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource != null && pickupSound != null)
            {
                audioSource.pitch += 0.5f;
                audioSource.PlayOneShot(pickupSound);
                audioSource.pitch -= 0.5f;
            }

            // Destruir la moneda
            Destroy(gameObject);
        }
    }

    // Update es llamado una vez por frame
    void Update()
    {
        // Rotar continuamente el objeto en el eje Z
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
