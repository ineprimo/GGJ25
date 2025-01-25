using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceComponent : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _maxBubbleGenerationTime = 0.05f;
    float _currentBubbleGenerationTime = 0.0f;

    private float _currentBubbleDamage = 0.0f;
    private float _currentBubbleLifeTime = 3.0f;

    [SerializeField] private GameObject _bubblePrefab;

    bool _canSinged = false;
    public void ActivateSigned() { _canSinged = true; }
    public void SetCurrentBubbleDamage(float dm) { _currentBubbleDamage = dm; }
    public void SetCurrentBubbleLifeTime(float tm) { _currentBubbleLifeTime = tm; }

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

            auxBubble.GetComponent<BubbleSignedComponent>().SetDamage(_currentBubbleDamage);
            auxBubble.GetComponent<LifeTimeComponent>().SetMaxDeleteTime(_currentBubbleLifeTime);

            _currentBubbleGenerationTime = 0.0f;
        }
        _currentBubbleGenerationTime += Time.deltaTime;
        //Debug.Log(_currentBubbleGenerationTime);
    }
}
