using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _agent;

    public void SetSpeed(float sp)
    {
        if (_agent != null)
        {
            _agent.speed = sp; // Cambia la velocidad de movimiento
        }
        else
        {
            Debug.LogWarning("NavMeshAgent no está asignado o encontrado.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameManager.Instance.GetPlayer().transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //_agent.destination = _playerTransform.position;
        //Debug.Log("Ir");
    }
}
