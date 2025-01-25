using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaquitaMovement : MonoBehaviour
{
    private GameObject player;
    //[SerializeField] public float speed;
    //[SerializeField] public float throwerDistance = 5.0f;
    //[SerializeField] public float closeCacaDistance = 1.0f;

    //bool thrower;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetPlayer();

        //thrower = GetComponent<CacaThrower>() != null;
        //Debug.Log(thrower);
    }

    // Update is called once per frame
    void Update()
    {
        // orientacion
        Vector3 v3 = player.transform.position - transform.position;
        v3.y = 0.0f; // para que se mantenga vertical
        transform.rotation = Quaternion.LookRotation(v3);


        /// *** seguimiento (antiguo) -> ahora lo hacemos en el IA Movement ***

        //float step = speed * Time.deltaTime;

        //Vector3 target = player.transform.position;
        //Vector3 direction = (player.transform.position - transform.position).normalized;
        //float distance = Vector3.Distance(transform.position, player.transform.position);

        //// si tiene el cacaThrower no llega hasta el player, mantiene distancia
        //if (thrower && distance > throwerDistance)
        //{
        //    target = transform.position + direction * step;
        //}
        //else if (!thrower && distance > closeCacaDistance)
        //{
        //    target = transform.position + direction * step;
        //}
        //else
        //{
        //    target = transform.position;
        //}

        //transform.position = Vector3.MoveTowards(transform.position, target, step);

    }
}
