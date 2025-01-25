using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform _door;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _toggleSpeed = 2f;
    private bool _isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    void Start()
    {
        _closedRotation = _door.localRotation;
        _openRotation = _closedRotation * Quaternion.Euler(0f, _openAngle, 0f);
    }

    public void ToggleDoor()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateDoor());
    }

    private IEnumerator AnimateDoor()
    {
        Quaternion targetRotation = _isOpen ? _closedRotation : _openRotation;
        _isOpen = !_isOpen;

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * _toggleSpeed;
            _door.localRotation = Quaternion.Lerp(_door.localRotation, targetRotation, elapsedTime);
            yield return null;
        }

        _door.localRotation = targetRotation;
    }
}
