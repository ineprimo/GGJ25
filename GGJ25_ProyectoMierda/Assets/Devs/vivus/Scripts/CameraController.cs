using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _maxFov = 100f;
    [SerializeField] private float _minFov = 60f;
    [SerializeField] private float _lerpSpeed = 5f; // Velocidad de la interpolación
    [SerializeField] private float _maxPlayerVel = 10.0f;
    private Camera _cameraComponent;
    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _cameraComponent = GetComponent<Camera>();
        _rigidBody = _player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InterpolateFOV();
    }

    void InterpolateFOV()
    {
        float auxPlayerSpeed = _rigidBody.velocity.magnitude;
        float auxPlayerNormalizedSpeed = math.clamp(auxPlayerSpeed / _maxPlayerVel, 0f, 1f);

        Debug.Log(auxPlayerNormalizedSpeed);

        float targetFOV = math.lerp(_minFov, _maxFov, auxPlayerNormalizedSpeed);
        _cameraComponent.fieldOfView = Mathf.Lerp(_cameraComponent.fieldOfView, targetFOV, Time.deltaTime * _lerpSpeed);        
    }
}
