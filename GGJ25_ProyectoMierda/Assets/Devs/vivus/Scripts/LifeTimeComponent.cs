using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeComponent : MonoBehaviour
{
    [SerializeField] private float _maxDeleteTime;
    [SerializeField] private float _currentDeleteTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentDeleteTime >= _maxDeleteTime)
        {
            Destroy(gameObject);
        }
        _currentDeleteTime += Time.deltaTime;
    }
}
