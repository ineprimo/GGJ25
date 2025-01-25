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

    // cuando toque el suelo la caca se deshace
    bool deshacer;
    [SerializeField] float deshacerTime = 3.0f;
    
    public float Damage { get {return _damage; } }

    // cuando la caca toca el suelo se destruye
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            deshacer = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();

        direction = player.transform.position - transform.position;
        direction.y += verticalBoost;

        deshacer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (deshacer)
        {
            gameObject.transform.position = transform.position;

            deshacerTime -= Time.deltaTime;
            if (deshacerTime < 0)
            {
                Destroy(gameObject);
            }

            transform.position = transform.position; // que se mantenga quieta en el suelo
        }
        else
        {
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }
    }
}
