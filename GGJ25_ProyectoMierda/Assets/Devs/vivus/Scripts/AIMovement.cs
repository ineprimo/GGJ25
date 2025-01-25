using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _agent;



    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameManager.Instance.GetPlayer().transform;
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null) Debug.Log("hay agent");
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _playerTransform.position;
        //Debug.Log("Ir");
    }

    public void SetSpeed(float sp)
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().speed = sp;
        }
        else
        {
            Debug.LogWarning("NavMeshAgent no está asignado o encontrado.");
        }
    }
}
