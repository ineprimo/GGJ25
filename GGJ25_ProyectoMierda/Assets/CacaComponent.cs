using UnityEngine;

public class CacaComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float verticalBoost = 2.0f;
    [SerializeField] private float limitTime = 1.5f; // Tiempo de vida del proyectil
    private Rigidbody rb;
    private Vector3 direction;

    public float Damage { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Transform player = GameManager.Instance.GetPlayer().transform;

        // Calcular dirección hacia el jugador con un pequeño ajuste vertical
        Vector3 targetPosition = player.position;
        targetPosition.y += verticalBoost; // Elevar un poco la altura del tiro
        direction = (targetPosition - transform.position).normalized;

        // Aplicar velocidad inicial al proyectil
        rb.velocity = direction * speed;

        // Destruir el proyectil después de un tiempo
        Destroy(gameObject, limitTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aplicar daño u otra lógica
            Debug.Log("¡Golpeó al jugador! Daño: " + Damage);
            collision.gameObject.GetComponent<PlayerMovement>().Hit(Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
