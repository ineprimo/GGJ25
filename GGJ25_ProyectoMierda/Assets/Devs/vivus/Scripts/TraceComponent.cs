using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceComponent : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _maxBubbleGenerationTime = 0.05f;
    float _currentBubbleGenerationTime = 0.0f;

    public float _currentBubbleDamage;
    private float _currentBubbleLifeTime = 3.0f;

    [SerializeField] private GameObject _bubblePrefab;

    private bool _canSinged;

    private bool _singed;
    public void ActivateSigned() {
        _canSinged = true; 

    }

    public void ActivateSinged()
    {
        _singed = true;

    }
    public void SetCurrentBubbleDamage(float dm) { _currentBubbleDamage = dm; }
    public void SetCurrentBubbleLifeTime(float tm) { _currentBubbleLifeTime = tm; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _canSinged = true; 
        _singed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_singed && 
            _maxBubbleGenerationTime <= _currentBubbleGenerationTime &&
            _rigidBody.velocity != Vector3.zero)
        {

            GameObject auxBubble = Instantiate(_bubblePrefab, transform.position, Quaternion.identity);
            Vector3 auxOffset = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f), 
                Random.Range(-0.5f, 0.5f));
            auxBubble.transform.position += auxOffset;

            auxBubble.GetComponent<BubbleSignedComponent>().SetDamage(_currentBubbleDamage);
            auxBubble.GetComponent<LifeTimeComponent>().SetMaxDeleteTime(_currentBubbleLifeTime);

            _currentBubbleGenerationTime = 0.0f;
        }
        _currentBubbleGenerationTime += Time.deltaTime;
    }
}
