using UnityEngine;

public class CacaComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float verticalBoost = 2.0f;
    [SerializeField] private float limitTime = 1.5f; // Tiempo de vida del proyectil
    private Vector3 direction;

    public float Damage { get; set; }

    void Start()
    {
        Transform player = GameManager.Instance.GetPlayer().transform;

        // Calcular dirección hacia el jugador
        direction = (player.position - transform.position).normalized;
        direction.y += verticalBoost; // Agregar un poco de altura al tiro

        // Destruir el proyectil después de un tiempo
        Invoke("DestroySelf", limitTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
