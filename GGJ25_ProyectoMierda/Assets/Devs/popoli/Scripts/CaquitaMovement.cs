using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaMovement : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // orientacion
        Vector3 v3 = player.transform.position - transform.position;
        v3.y = 0.0f; // para que se mantenga vertical
        transform.rotation = Quaternion.LookRotation(v3);
    }
}
