using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // seguimiento
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        // orientacion
        Vector3 v3 = player.transform.position - transform.position;
        v3.y = -90.0f; // para que se mantenga vertical
        transform.rotation = Quaternion.LookRotation(v3);
    }
}
