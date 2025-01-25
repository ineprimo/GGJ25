using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacaComponent : MonoBehaviour
{
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float verticalBoost = 2.0f;

    Vector3 direction;
    GameObject player;
    
    public float Damage { get {return _damage; } }

    // cuando la caca toca el suelo se destruye
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();

        direction = player.transform.position - transform.position;
        direction.y += verticalBoost;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * (speed * Time.deltaTime);
    }
}
