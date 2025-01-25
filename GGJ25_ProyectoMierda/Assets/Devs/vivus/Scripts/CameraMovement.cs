using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _mouseSensitivity = 2f;
    private float _cameraVerticalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RotateCamera(float myX, float myY)
    {
        float auxX = myX * _mouseSensitivity;
        float auxY = myY * _mouseSensitivity;

        _cameraVerticalRotation -= auxY;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;

        _player.Rotate(Vector3.up * auxX);
    }
}
