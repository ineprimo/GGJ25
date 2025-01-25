using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceComponent : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _maxBubbleGenerationTime = 0.05f;
    float _currentBubbleGenerationTime = 0.0f;

    [SerializeField] private GameObject _bubblePrefab;

    bool _canSinged = false;
    public void ActivateSigned() { _canSinged = true; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_canSinged && 
            _maxBubbleGenerationTime <= _currentBubbleGenerationTime &&
            _rigidBody.velocity != Vector3.zero)
        {
            GameObject auxBubble = Instantiate(_bubblePrefab, transform.position, Quaternion.identity);
            Vector3 auxOffset = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f), 
                Random.Range(-0.5f, 0.5f));
            auxBubble.transform.position += auxOffset;
            _currentBubbleGenerationTime = 0.0f;
        }
        _currentBubbleGenerationTime += Time.deltaTime;
    }
}
