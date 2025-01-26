using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShake : MonoBehaviour
{
    [SerializeField] private float _amplitudMovimiento = 0.2f;
    [SerializeField] private float _velocidadMovimiento = 3f;

    private Vector3 _posicionOriginal;
    private float _offsetX;
    private float _offsetY;

    void Start()
    {
        _posicionOriginal = transform.position;
        _offsetX = Random.Range(0f, 2f * Mathf.PI);
        _offsetY = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float movimientoX = Mathf.Sin(Time.time * _velocidadMovimiento + _offsetX) * _amplitudMovimiento;
        float movimientoY = Mathf.Cos(Time.time * _velocidadMovimiento + _offsetY) * _amplitudMovimiento;

        transform.position = _posicionOriginal + new Vector3(movimientoX, movimientoY, 0f);
    }
}
