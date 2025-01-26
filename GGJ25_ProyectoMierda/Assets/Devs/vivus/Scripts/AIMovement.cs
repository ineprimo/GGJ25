using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Transform _playerTransform;
    public NavMeshAgent _agent;

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
        if (_agent.isOnNavMesh)
        {
            _agent.destination = _playerTransform.position;
        }
    }

    public void SetSpeed(float sp)
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().speed = sp;
        }
    }
}
