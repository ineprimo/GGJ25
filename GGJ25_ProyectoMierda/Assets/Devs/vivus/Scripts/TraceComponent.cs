using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceComponent : MonoBehaviour
{
    [SerializeField] private float _maxBubbleGenerationTime = 0.5f;
    float _currentBubbleGenerationTime = 0.0f;

    [SerializeField] private GameObject _bubblePrefab;

    bool _canSinged = false;
    public void ActivateSigned() { _canSinged = true; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_maxBubbleGenerationTime <= _currentBubbleGenerationTime)
        {
            Instantiate(_bubblePrefab, transform.position, Quaternion.identity);
            _currentBubbleGenerationTime = 0.0f;
        }
    }
}
